using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ContactController : Controller
	{
		private readonly IContactService _contactService;

		public ContactController(IContactService contactService)
		{
			_contactService = contactService;
		}

		public IActionResult Index()
		{
			var values = _contactService.GetAll();
			return View(values);
		}

		public IActionResult DeleteContact(int id) 
		{
			var contact = _contactService.GetById(id);
			if(contact!=null)
			{
				_contactService.TDelete(contact);
			}
			
			return RedirectToAction("Index");
		}



	}
}
