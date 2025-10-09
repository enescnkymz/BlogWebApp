using DateAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreProject.Controllers
{
	public class LoginController : Controller
	{
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Index(Writer w)                                  //Metot asenkron bir işlem barındırğı için dönüş tipi task olur
		{
			Context c = new Context();
			var LoginValue = c.Writers.FirstOrDefault(x => x.WriterName == w.WriterName && x.WriterPassword == w.WriterPassword);
			if (LoginValue != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,w.WriterName)                                //Claim kullanıcı bilgilerini içerir
				};
				var userIdentity = new ClaimsIdentity(claims, "a");                        //ClaimsIdentity TC Kimlik gibi düşünebilirsin
				ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);             //Kişinin tamamını temsil eder
				await HttpContext.SignInAsync(principal);                                  //Kulanıcıyı giriş yapmış hale getiriyor (Arka planda işlemlere devam et-I/O işlemi olduğu için)
												
				return RedirectToAction("Index","Description");
						
			}
			else
			{
				return View();
			}


		}
	}
}
