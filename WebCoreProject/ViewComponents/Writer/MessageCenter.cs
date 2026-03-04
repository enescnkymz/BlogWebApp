using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreProject.ViewComponents.Writer
{
	public class MessageCenter : ViewComponent
	{
		MessageManager mm = new MessageManager(new EfMessageRepository());
		public IViewComponentResult Invoke()
		{
			var currentUserId = int.Parse(UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
			var values = mm.GetLast4Messages(currentUserId);
			return View(values);
		}

	}
}
