using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreProject.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDescriptionService _descriptionService;
		private readonly ICategoryService _categoryService;

		public DashboardController(IDescriptionService descriptionService, ICategoryService categoryService)
		{
			_descriptionService = descriptionService;
			_categoryService = categoryService;
		}

		public IActionResult Index()
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			int ViewCount = _descriptionService.GetDescriptionsByWriter(userId).Sum(x => x.DescriptionViewCount);
			ViewBag.Earning = 0.18 * ViewCount;
			ViewBag.ViewCount= ViewCount;
			return View();
		}
	}
}
