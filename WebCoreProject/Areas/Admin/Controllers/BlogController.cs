using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using ClosedXML.Excel;
using CoreLayer.Helpers.Abstract;
using DateAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace WebCoreProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class BlogController : Controller
	{
		private readonly ICommentService _commentService;
		private readonly IDescriptionService _descriptionService;
		private readonly IExcelHelper _excelService;
		

		public BlogController(ICommentService commentService, IDescriptionService descriptionService, IExcelHelper excelService)
		{
			_commentService = commentService;
			_descriptionService = descriptionService;
			_excelService = excelService;
		}

		public IActionResult ExportExcelBlogList()
		{
			// 1. Veriyi Getir (Manager'dan)
			var blogListDto = _descriptionService.GetExcelBlogList();

			// 2. Excel'e Çevir (Helper'dan)
			var fileContent = _excelService.ExportToExcel(blogListDto, "Blog Listesi");

			// 3. İndir
			return File(
				fileContent,
				"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				$"BlogListesi_{DateTime.Now:dd-MM-yyyy}.xlsx"
			);
		}



		public IActionResult Index(int page=1)
		{
			var posts = _descriptionService.GetAll();
			var pagedPosts = posts.ToPagedList(page , 12);
			return View(pagedPosts);

		}

		[HttpPost]
		public IActionResult DeletePost(int id) 
		{ 
			var post = _descriptionService.GetById(id);
			if (!(post == null)) 
			{
				if (!string.IsNullOrEmpty(post.DescriptionImage))
				{
					
					string fileName = System.IO.Path.GetFileName(post.DescriptionImage);

					string wwwRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
					string oldPath = System.IO.Path.Combine(wwwRootPath, "BlogImages", fileName);

					
					if (System.IO.File.Exists(oldPath))
					{
						System.IO.File.Delete(oldPath); 
					}
				}



				_commentService.DeleteCommentsByPostId(id);
				_descriptionService.TDelete(post);
                 return Json(new {success=true , message="Silme İşlemi Tamamlandı!"});
			}

			return Json(new { success = false, message = "Yazı Bulunamadı!" });

		}




	}
}
