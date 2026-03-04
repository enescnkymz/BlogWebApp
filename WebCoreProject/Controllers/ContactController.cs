using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.Controllers
{

	[AllowAnonymous]
	public class ContactController : Controller
	{
		ContactManager cm = new ContactManager(new EfContactRepository());
		
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(Contact c)
		{
			
			c.ContactStatus = true;
			c.ContactDate = DateTime.Now;
			cm.AddContact(c);
			TempData["Success"] = "Mesajınız gönderilmiştir!";
			return RedirectToAction("Index", "Contact");
				
		}

	}
}
