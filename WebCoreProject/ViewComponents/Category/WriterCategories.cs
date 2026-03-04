using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DateAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebCoreProject.Models;

namespace WebCoreProject.ViewComponents.Category
{
	public class WriterCategories : ViewComponent
	{
		private readonly ICategoryService _categoryService;
		private readonly IDescriptionService _descriptionService;

		public WriterCategories(ICategoryService categoryService, IDescriptionService descriptionService)
		{
			_categoryService = categoryService;
			_descriptionService = descriptionService;
		}

		public IViewComponentResult Invoke()
		{
			var currentUserId = int.Parse(UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier));

			// 1. Yazara ait tüm yazıları çek
			var writerArticles = _descriptionService.GetDescriptionsWithCategory().Where(x => x.WriterID == currentUserId).ToList();
			int totalArticles = writerArticles.Count();

			// 2. Yazıları kategorilerine göre grupla ve listeyi oluştur
			var stats = writerArticles
				.GroupBy(x => x.Category.CategoryName)
				.Select((group) => new CategoryRateViewModel
				{
					CategoryName = group.Key,					
					Oran = totalArticles > 0 ? (double)group.Count() / totalArticles * 100 : 0,
					
				}).ToList();

			return View(stats); 

		}


	}

}
