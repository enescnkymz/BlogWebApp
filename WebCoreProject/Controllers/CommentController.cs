using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.Controllers
{
	public class CommentController : Controller
	{
		CommentManager cm = new CommentManager(new EfCommentRepository());

		[HttpGet]
		public PartialViewResult AddComment()
		{
			return PartialView();

		}
		[HttpPost]
		public IActionResult AddComment(Comment c)
		{
			c.CommentStatus = true;
			c.CommentDate = DateTime.Today;			
			cm.AddComment(c);
			return Ok();

		}


	}
}
