using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;

namespace WebCoreProject.Controllers
{
    public class AboutController : Controller
    {
        AboutManager am = new AboutManager(new EfAboutRepository());
        public IActionResult Index()
        {
            var value = am.GetAbouts();
            return View(value);
        }

   
    }
}
