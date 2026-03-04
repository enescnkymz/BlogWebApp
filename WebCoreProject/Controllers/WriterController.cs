using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DateAccessLayer.EntityFramework;
using DateAccessLayer.Migrations;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using CoreLayer.Helpers;

namespace WebCoreProject.Controllers
{
	public class WriterController : Controller
	{
		private readonly IWriterService _writerService;
		private readonly ICommentService _commentService;
		private readonly IDescriptionService _descriptionService;


		public WriterController(IWriterService writerService , ICommentService commentService , IDescriptionService descriptionService)
		{
			_writerService = writerService;
			_commentService = commentService;
			_descriptionService = descriptionService;
		}


		public IActionResult Test()
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			ViewBag.UserId = userId;
			return View();
		}

		public PartialViewResult WriterLayoutNavbar()
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			ViewBag.UserId = userId;
			return PartialView();
		}

		public PartialViewResult WriterPageFooter()
		{
			return PartialView();
		}

		public IActionResult WriterProfilePage(int id)
		{

				
			    var ProfileId = id;	
			    if(ProfileId==0)
		     	 {
				  ProfileId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			     }
			    var value = _writerService.GetById(ProfileId);
			    ViewBag.ViewCount = _descriptionService.GetDescriptionsByWriter(ProfileId).Sum(x=>x.DescriptionViewCount);	
		     	ViewBag.BlogCount = _descriptionService.GetBlogCountByWriter(ProfileId);
			    ViewBag.CommentCount = _commentService.GetCommentCountById(ProfileId);
			    return View(value);
		}

		[HttpGet]
		public IActionResult WriterProfileEdit()
		{
			var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var value = _writerService.GetById(currentUserId);
			return View(value);
		}
		[HttpPost]
		public async Task<IActionResult> WriterProfileEditAsync(Writer w, string ConfirmPassword, IFormFile WriterImage)
		{
			var existingWriter = _writerService.GetById(w.WriterID);

			WriterValidator Wv = new WriterValidator();
			ValidationResult results = Wv.Validate(w);

			if (results.IsValid && w.WriterPassword == ConfirmPassword)
			{
				
				if (!string.IsNullOrEmpty(w.WriterPassword))
				{
					existingWriter.WriterPassword = w.WriterPassword;
				}

				if (WriterImage != null)
				{
					if (!string.IsNullOrEmpty(existingWriter.WriterImage))
					{
										
						// 1. Veritabanındaki "/WriterImages/abc.png" ifadesinden sadece "abc.png" kısmını al
						string fileName = System.IO.Path.GetFileName(existingWriter.WriterImage);

						// 2. Fiziksel yolu doğru birleştir (Başında slash olmadan "WriterImages" klasörüne git)
						string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
						string oldPath = Path.Combine(wwwRootPath, "WriterImages", fileName);

						// Fiziksel olarak o dosya klasörde var mı bak
						if (System.IO.File.Exists(oldPath))
						{
							System.IO.File.Delete(oldPath); // Varsa dosyayı sil
						}
					}
				}

					if (WriterImage != null && WriterImage.Length > 0)
				 {

					var newImageName = await FileHelper.SaveAndCompressImage(WriterImage, "WriterImages");
					existingWriter.WriterImage = "/WriterImages/" + newImageName;

				 }
 
				existingWriter.NameSurname = w.NameSurname;
				existingWriter.WriterName = w.WriterName;
				existingWriter.WriterAbout = w.WriterAbout;
				existingWriter.WriterMail = w.WriterMail;

				_writerService.TUpdate(existingWriter);

				return RedirectToAction("Index", "Dashboard");
			}
			else
			{
				
				foreach (var items in results.Errors)
				{
					ModelState.AddModelError(items.PropertyName, items.ErrorMessage);
				}
				
				if (!string.IsNullOrEmpty(w.WriterPassword) && w.WriterPassword != ConfirmPassword)
				{
					ModelState.AddModelError("ConfirmPassword", "Şifreler uyuşmuyor!");
				}

				return View();
			
			}
		}

	}
}
