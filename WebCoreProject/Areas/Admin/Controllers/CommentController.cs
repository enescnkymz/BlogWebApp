using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;


namespace WebCoreProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;

		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		public IActionResult Index(int page=1)
		{
			var values = _commentService.GetCommentsForAdmin();
			var pagedValues= values.ToPagedList(page,20);
			return View(pagedValues);

		}

		[HttpGet]
		public IActionResult DeleteComment(int id)
		{
			var existingComment = _commentService.GetById(id);
			if (existingComment != null)
			{
				_commentService.TDelete(existingComment);
			}

			return RedirectToAction("Index");

		}




	}
}
