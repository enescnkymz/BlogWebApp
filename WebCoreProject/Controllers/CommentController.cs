using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreProject.Controllers
{
	public class CommentController : Controller
	{
		CommentManager cm = new CommentManager(new EfCommentRepository());

		[HttpGet]
		public PartialViewResult AddComment(int id)
		{
			ViewBag.Id = id;
			return PartialView();

		}

		[HttpPost]
		public IActionResult AddComment(Comment c)
		{

			var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

			c.CommentSenderID = currentUserId;
			c.BlogRate = 0;
			c.CommentStatus = true;
			c.CommentDate = DateTime.Now;
			cm.TAdd(c);
			return Ok();

		}


	}
}
