using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreProject.Controllers
{
	public class NotificationController : Controller
	{
		NotificationManager nm = new NotificationManager(new EfNotificationRepository());
		public IActionResult AllNotifications()
		{
			var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var values = nm.GetAllByID(currentUserId);
			return View(values);
		}
	}
}
