using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ChartController : Controller
	{

		private readonly ICategoryService _categoryService;
		public ChartController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}


		public IActionResult Index()
		{
			return View();
		}



		[HttpGet]
		public IActionResult GetChartData()
		{
			
			var chartData = _categoryService.GetCategoryChart();

			var jsonResult = chartData.Select(x => new
			{
				kategori = x.CategoryName,
				oran = x.BlogCount
			}).ToList();

			return Json(jsonResult);
		}

		

	}
}
