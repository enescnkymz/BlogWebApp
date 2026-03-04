using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.ViewComponents.Description
{
	public class OtherPosts : ViewComponent
	{
		DescriptionManager dm = new DescriptionManager(new EfDescriptionRepository());
		public IViewComponentResult Invoke(int id)
		{
			var values = dm.WriterLast3Post(id);
		    return View(values);
		}
	}
}
