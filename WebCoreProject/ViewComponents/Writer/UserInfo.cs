using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebCoreProject.ViewComponents.Writer
{
	public class UserInfo : ViewComponent
	{
		WriterManager wm = new WriterManager(new EfWriterRepository());

	    public IViewComponentResult Invoke()
		{
			var currentUserId = int.Parse(UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
			var UserInfo = wm.GetUserNavbarInfoById(currentUserId);
			return View(UserInfo);
		}

	}
}
