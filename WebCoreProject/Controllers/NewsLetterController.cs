using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace WebCoreProject.Controllers
{
	public class NewsLetterController : Controller
	{
		NewsLetterManager nm = new NewsLetterManager(new EfNewsLetterRepository());
		[HttpGet]
		public IActionResult SubscribeNewsLetter()
		{
			return PartialView();
		}
		[HttpPost]
		public IActionResult SubscrieNewsLetter(NewsLetter n)
		{
			n.MailStatus = true;
			nm.addNewsLetter(n);
			return Ok(); 
		}
	}
}
