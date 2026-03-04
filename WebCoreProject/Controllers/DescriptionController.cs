using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DateAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Vml.Office;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using X.PagedList.Extensions;
using CoreLayer.Helpers;

namespace WebCoreProject.Controllers
{
	[AllowAnonymous]
	public class DescriptionController : Controller
	{
		private readonly IDescriptionService dm;
		private readonly ICategoryService cm;
		private readonly IWriterService wm;

		public DescriptionController(IDescriptionService descriptionService, ICategoryService categoryService, IWriterService writerService)
		{
			dm = descriptionService;
			cm = categoryService;
			wm = writerService;
		}

		public IActionResult Index(int? categoryId , int? writerId ,  string search , int page=1)
		{
			var values = dm.GetDescriptionsWithCategory().AsQueryable();

			if (categoryId.HasValue)
			{
				values = values.Where(x => x.CategoryID == categoryId.Value);
				ViewBag.CategoryName = cm.GetById(categoryId.Value).CategoryName;
			}

			if (!string.IsNullOrEmpty(search))
			{
				var lowerSearch=search.ToLower();

				values = values.Where(x => x.DescriptionTitle.ToLower().Contains(lowerSearch) || x.DescriptionContent.ToLower().Contains(lowerSearch));
				ViewBag.Search = search;
			}

			if(writerId.HasValue)
			{
				values = values.Where(x=>x.WriterID == writerId.Value);
				ViewBag.WriterName = wm.GetById(writerId.Value).NameSurname;
			}

			if (page < 1)
			{
				page = 1;
			}

			var list = values.ToPagedList(page, 12);
            return View(list);

		}

		public IActionResult BlogDetails(int id)
		{
			if(id == 0)
		    {
              return RedirectToAction("Index");
			}

			ViewBag._id = id;
			var value = dm.GetDescriptionWithCategory(id);
			value.DescriptionViewCount++;
			dm.TUpdate(value);
			return View(value);

		}
		public IActionResult WriterPosts(int page=1)
		{
			var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var values = dm.GetDescriptionsByWriterWithCategory(currentUserId);
			var pagedValues =values.ToPagedList(page, 10);
			return View(pagedValues);
		}

		[HttpGet]
		public IActionResult AddBlog()
		{

			List<SelectListItem> CMenu = (from x in cm.GetAll()
										  select new SelectListItem    //Burada LINQ sorgusu ile her kategori (x) için yeni bir SelectListItem oluşturuluyor:
										  {
											  Text = x.CategoryName,
											  Value = x.CategoryID.ToString()
										  }
													 ).ToList();
			ViewBag.CtgryMenu = CMenu;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddBlogAsync(Description d, IFormFile imageFile)
		{

			BlogValidator Bv = new BlogValidator();
			ValidationResult result = Bv.Validate(d);
			if (result.IsValid)
			{
				d.DescriptionCreateDate = DateTime.Now;
				d.WriterID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); ;
				d.DescriptionStatus = 1;				

				if (imageFile != null)
				{

					var newImageName = await FileHelper.SaveAndCompressImage(imageFile , "BlogImages");
					d.DescriptionImage = "/BlogImages/" + newImageName; 

				}
				else
				{
					d.DescriptionImage = "/BlogImages/default.png";	
				}

				dm.TAdd(d);	
				
				return RedirectToAction("Index", "Description");

			}
			else
			{
				List<SelectListItem> CMenu = (from x in cm.GetAll()
											  select new SelectListItem
											  {
												  Text = x.CategoryName,
												  Value = x.CategoryID.ToString()
											  }).ToList();

				ViewBag.CtgryMenu = CMenu;
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}

			}
			return View();


		}

		public IActionResult DeleteBlog(int id)
		{

			
			var BlogValue = dm.GetById(id);

			if (!string.IsNullOrEmpty(BlogValue.DescriptionImage))
			{
				
				string fileName = System.IO.Path.GetFileName(BlogValue.DescriptionImage);
				
				string wwwRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
				string oldPath = System.IO.Path.Combine(wwwRootPath, "BlogImages", fileName);

			
				if (System.IO.File.Exists(oldPath))
				{
					System.IO.File.Delete(oldPath); 
				}
			}
			dm.TDelete(BlogValue);
			return RedirectToAction("WriterPosts");

		}

		[HttpGet]
		public IActionResult EditBlog(int id)
		{
			var value = dm.GetById(id);
			List<SelectListItem> CMenu = (from x in cm.GetAll()
										  select new SelectListItem    //Burada LINQ sorgusu ile her kategori (x) için yeni bir SelectListItem oluşturuluyor:
										  {
											  Text = x.CategoryName,
											  Value = x.CategoryID.ToString()
										  }
													 ).ToList();
			ViewBag.CtgryMenu = CMenu;
			return View(value);
		}
		[HttpPost]
		public IActionResult EditBlog(Description d, IFormFile newImage)
		{
			
			var oldBlog = dm.GetById(d.DescriptionID);
			oldBlog.DescriptionTitle = d.DescriptionTitle;
			oldBlog.DescriptionContent = d.DescriptionContent;
			oldBlog.CategoryID= d.CategoryID;

			if (newImage != null)
			{
				if (!string.IsNullOrEmpty(oldBlog.DescriptionImage))
				{
					// 1. Veritabanındaki "/WriterImages/abc.png" ifadesinden sadece "abc.png" kısmını al
					string fileName = System.IO.Path.GetFileName(oldBlog.DescriptionImage);

					// 2. Fiziksel yolu doğru birleştir (Başında slash olmadan "WriterImages" klasörüne git)
					string wwwRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
					string oldPath = System.IO.Path.Combine(wwwRootPath, "BlogImages", fileName);

					// Fiziksel olarak o dosya klasörde var mı bak
					if (System.IO.File.Exists(oldPath))
					{
						System.IO.File.Delete(oldPath); // Varsa dosyayı sil
					}
				}


			var newFileName = FileHelper.SaveAndCompressImage(newImage, "BlogImages");

			// Veritabanına gidecek olan kısa yolu set et
			oldBlog.DescriptionImage = "/BlogImages/" + newFileName;

			}


			dm.TUpdate(oldBlog);			
			return RedirectToAction("WriterPosts");
		
		
		}









	}
}
