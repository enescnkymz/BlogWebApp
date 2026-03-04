using BusinessLayer.Abstract;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net.NetworkInformation;
using X.PagedList;

namespace WebCoreProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class WriterController : Controller
	{
		private readonly IWriterService _writerService;
		private readonly IMessageService _messageService;
		private readonly IDescriptionService _descriptionService;
		private readonly ICommentService _commentService;

		public WriterController(IWriterService writerService, IMessageService messageService, IDescriptionService descriptionService, ICommentService commentService)
		{
			_writerService = writerService;
			_messageService = messageService;
			_descriptionService = descriptionService;
			_commentService = commentService;
		}

		public async Task<IActionResult> Index(int page = 1,string search=null)
		{
			int pageSize = 12;
			var writers = await _writerService.GetWritersWithStatistics(page, pageSize , search);
			ViewBag.CurrentSearch = search;
			return View(writers);
		}


		[HttpPost]
		public IActionResult DeleteWriter(int id)
		{
			var writer = _writerService.GetById(id);
			_messageService.DeleteWriterMessagesByWriterId(id);
			_commentService.DeleteCommentsByWriterId(id);

			var writerBlogs = _descriptionService.GetDescriptionsByWriter(id);
			string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");


			foreach (var blog in writerBlogs)
			{
				if (!string.IsNullOrEmpty(blog.DescriptionImage))
				{
					string blogFileName = Path.GetFileName(blog.DescriptionImage);
					string blogPath = Path.Combine(wwwRootPath, "BlogImages", blogFileName); 

					if (System.IO.File.Exists(blogPath))
					{
						System.IO.File.Delete(blogPath);
					}
				}


				_commentService.DeleteCommentsByPostId(blog.DescriptionID);
				_descriptionService.TDelete(blog);
			}



			if (!string.IsNullOrEmpty(writer.WriterImage))
			{

				string fileName = System.IO.Path.GetFileName(writer.WriterImage);			
				string oldPath = System.IO.Path.Combine(wwwRootPath, "WriterImages", fileName);

				if (System.IO.File.Exists(oldPath))
				{
					System.IO.File.Delete(oldPath);
				}
			}
			_writerService.TDelete(writer);
			return Json(new { success = true, message = "Yazar Silindi!" });
		}

		[HttpPost]
		public IActionResult ChangeStatus(int id)
		{
			var writer = _writerService.GetById(id);
			writer.WriterStatus = !writer.WriterStatus; 
			_writerService.TUpdate(writer);
			return Json(new { success = true, message = "Düzenleme Başarılı" });
		}


	}
}
