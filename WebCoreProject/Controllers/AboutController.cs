using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;

namespace WebCoreProject.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        AboutManager am = new AboutManager(new EfAboutRepository());
        public IActionResult Index()
        {
            var value = am.GetAll();
            return View(value);
        }

   
    }
}
