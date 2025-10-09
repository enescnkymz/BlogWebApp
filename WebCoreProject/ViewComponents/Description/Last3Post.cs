using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.ViewComponents.Description
{
    public class Last3Post : ViewComponent
    {
        DescriptionManager dm = new DescriptionManager(new EfDescriptionRepository());

        public IViewComponentResult Invoke()
        {
            var values = dm.GetLast3Post();
            return View(values);
        }


    }
}
