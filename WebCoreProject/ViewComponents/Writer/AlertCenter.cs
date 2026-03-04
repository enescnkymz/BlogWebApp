using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreProject.ViewComponents.Writer
{
	public class AlertCenter : ViewComponent
	{
		NotificationManager nm = new NotificationManager(new EfNotificationRepository());
		public IViewComponentResult Invoke()
		{
			var currentUserId = int.Parse(UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));
			var values = nm.GetLast4NotificationByWriterID(currentUserId);
            return View(values);
		}
	}
}
