using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreProject.ViewComponents.Description
{
	public class BlogCount : ViewComponent
	{
		DescriptionManager dm = new DescriptionManager(new EfDescriptionRepository());
		public IViewComponentResult Invoke()
		{
			var currentUserId = int.Parse(UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
			var BlogCount = dm.GetBlogCountByWriter(currentUserId);
			ViewBag.BlogCount = BlogCount;
			return View();

		}
	}
}
