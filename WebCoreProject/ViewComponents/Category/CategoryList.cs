using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.ViewComponents.Category
{
	public class CategoryList:ViewComponent
	{
		CategoryManager cm = new CategoryManager(new EfCategoryRepository());
		public IViewComponentResult Invoke()
		{
			var values = cm.GetAll();
			return View(values);
		}


	}
}
