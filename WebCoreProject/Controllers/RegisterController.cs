using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebCoreProject.Models;

namespace WebCoreProject.Controllers
{
	public class RegisterController : Controller
	{
		WriterManager wm = new WriterManager(new EfWriterRepository());
		
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]		
		public IActionResult Index(Writer b)
		{

			WriterValidator Wv = new WriterValidator();
			ValidationResult results = Wv.Validate(b);
			if (results.IsValid)
			{
				b.WriterStatus = true;
				b.WriterAbout = "açıklama";
				wm.AddWriter(b);
				return RedirectToAction("Index", "Description");
			}
			else 
			{
                foreach (var item in results.Errors)
                {
                 ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
				
            }
			return View();

		}


}
}
