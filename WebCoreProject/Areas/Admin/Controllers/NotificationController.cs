using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class NotificationController : Controller
	{
		private readonly INotificationService _notificationService;

		public NotificationController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		public IActionResult Index()
		{
			var values=_notificationService.GetAll();
			return View(values);
		}

		public ActionResult DeleteNotification(int id) 
		{			
			var ntc = _notificationService.GetById(id);
			_notificationService.TDelete(ntc);
			return RedirectToAction("Index");
		}

		public ActionResult AddNotification(Notification n) 
		{
			n.NotificationDate = DateTime.Now;
			n.NotificationStatus=true;
			
			_notificationService.TAdd(n);
			return RedirectToAction("Index");
		}





	}
}
