using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.ViewComponents.Comment
{
	public class CommentListByPost : ViewComponent
	{
		CommentManager cm = new CommentManager(new EfCommentRepository());
		public IViewComponentResult Invoke(int id)
		{
			var Values = cm.GetCommentsWithWriterByBlogId(id);
			return View(Values);
		}
	
	}
}
