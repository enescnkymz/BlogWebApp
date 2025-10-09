using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.Controllers
{
	[AllowAnonymous]
	public class DescriptionController : Controller
	{
		DescriptionManager dm = new DescriptionManager(new EfDescriptionRepository());
		
		public IActionResult Index()
		{
			var values = dm.GetDescriptionsWithCategory();
			return View(values);
		}

		public IActionResult BlogDetails(int id) 
		{
			
			ViewBag._id = id;
			var value = dm.GetDescriptionWithCategory(id);
			return View(value);
		
		}
		

	}
}
