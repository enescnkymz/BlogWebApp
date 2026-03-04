using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebCoreProject.Models;

namespace WebCoreProject.Controllers
{
	[AllowAnonymous]
	public class RegisterController : Controller
	{

		private readonly IWriterService _writerService;
		public RegisterController(IWriterService writerService) {
			_writerService = writerService;
		}
		
		//private readonly UserManager<AppUser> _userManager;

		//public RegisterController(UserManager<AppUser> userManager)
		//{
		//	_userManager = userManager;
		//}




		
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}


		//[HttpPost]
		//public async Task<IActionResult> Index(UserSignUpViewModel p)
		//{

		//	if (ModelState.IsValid)   //Formdan gelen veriler, ViewModel üzerinde tanımlı kurallara uyuyorsa
		//	{
		//		// ViewModel -> AppUser Dönüşümü
		//		AppUser user = new AppUser()
		//		{

		//			Email = p.Mail,
		//			UserName = p.Username,


		//		};

		//		// Kullanıcı oluşturma (Şifreyi Hash'ler)
		//		var result = await _userManager.CreateAsync(user, p.Password);

		//		if (result.Succeeded)
		//		{
		//			// Başarılıysa Login ekranına git
		//			return RedirectToAction("Index", "Login");
		//		}
		//		else
		//		{
		//			// Hata varsa (Şifre basit, mail kayıtlı vs.) hataları modele ekle
		//			foreach (var item in result.Errors)
		//			{
		//				ModelState.AddModelError("", item.Description);
		//			}
		//		}
		//	}

		//	// Eğer buraya geldiyse hata vardır, formu verilerle birlikte geri göster
		//	return View(p);
		//}


		[HttpPost]
		public IActionResult Index(UserSignUpViewModel p)
		{
			
			if (ModelState.IsValid)
			{
				
				Writer w = new Writer();

				w.NameSurname = p.NameSurname;
				w.WriterName = p.Username; 
				w.WriterMail = p.Mail;
				w.WriterPassword = p.Password; 
				w.WriterStatus = true; 
				w.WriterImage = "/images/default.png";
				w.UserRole = UserRole.Yazar;

		
				_writerService.TAdd(w);

				return RedirectToAction("Index", "Login");
			}

			return View(p);
		}




	}
}
