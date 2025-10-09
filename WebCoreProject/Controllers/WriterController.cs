using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.Controllers
{
	public class WriterController : Controller
	{
		[AllowAnonymous]
		public IActionResult Test()
		{
			return View();
		}
	}
}
