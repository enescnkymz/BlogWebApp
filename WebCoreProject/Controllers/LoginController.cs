using DateAccessLayer.Concrete;
using DocumentFormat.OpenXml.Spreadsheet;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebCoreProject.Models;

namespace WebCoreProject.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		//private readonly SignInManager<AppUser> _signInManager;

		//public LoginController(SignInManager<AppUser> signInManager)
		//{
		//	_signInManager = signInManager;
		//}

		public IActionResult Index()
		{
			return View();
		}


		//[HttpPost]
		//public async Task<IActionResult> Index(UserSignInViewModel p)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);
		//		if (result.Succeeded)
		//		{
		//			return RedirectToAction("Index", "Description");
		//		}
		//		else
		//		{
		//			ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre.");
		//		}
		//	}
		//	return View();
		//}

		[HttpPost]
		public async Task<IActionResult> Index(UserSignInViewModel w)                                  //Metot asenkron bir işlem barındırğı için dönüş tipi task olur
		{
			Context c = new Context();
			var LoginValue = c.Writers.FirstOrDefault(x => x.WriterName == w.Username && x.WriterPassword == w.Password);
			
			if (LoginValue != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,LoginValue.NameSurname),                            //Claim kullanıcı bilgilerini içerir
					new Claim(ClaimTypes.NameIdentifier, LoginValue.WriterID.ToString()),
					new Claim(ClaimTypes.Role, LoginValue.UserRole.ToString())
				};
				var userIdentity = new ClaimsIdentity(claims, "a");                        //ClaimsIdentity TC Kimlik gibi düşünebilirsin
				ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);             //Kişinin tamamını temsil eder
				await HttpContext.SignInAsync(principal);                                  //Kulanıcıyı giriş yapmış hale getiriyor (Arka planda işlemlere devam et-I/O işlemi olduğu için)

				return RedirectToAction("Index", "Description");

			}
			else
			{
				ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı!");
				return View(w);
			}

		}
		public async Task<IActionResult> LogOut()
		{
			// 1. Çerezi siliyoruz (Oturumu kapatır)
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// 2. Giriş sayfasına geri gönderiyoruz
			return RedirectToAction("Index", "Description");

		}





	}
}
