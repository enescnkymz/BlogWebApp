using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DateAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using X.PagedList;
using X.PagedList.Extensions;

namespace WebCoreProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
	{

		private readonly ICategoryService cm;
		private readonly IDescriptionService _descriptionService;
		private readonly ICommentService _commentService;

		public CategoryController(ICategoryService categoryService, IDescriptionService descriptionService, ICommentService commentService)
		{
			cm = categoryService;
			_descriptionService = descriptionService;
			_commentService = commentService;
		}

		public IActionResult Index(int page = 1)
		{

			var values = cm.GetAll().ToPagedList(page, 10);
			return View(values);

		}

		[HttpGet]
		public IActionResult AddCategory()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddCategory(Category c)
		{
			CategoryValidator cv = new CategoryValidator();
			ValidationResult results = cv.Validate(c);
			if (results.IsValid)
			{

				cm.TAdd(c);
				return RedirectToAction("Index", "Category");
			}
			else
			{
				foreach (var item in results.Errors)
				{
					ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
				}


			}

			return View();
		}


		public IActionResult ChangeCategoryStatus(int id)
		{
			var kategori = cm.GetById(id);
			kategori.CategoryStatus = !kategori.CategoryStatus;
			cm.TUpdate(kategori);
			return RedirectToAction("Index");

		}

		[HttpPost]
		public IActionResult DeleteCategory(int id)
		{

			string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

			var posts = _descriptionService.GetDescriptionsByCategoryId(id);

			foreach (var post in posts)
			{

				_commentService.DeleteCommentsByPostId(post.DescriptionID);
				
				if (!string.IsNullOrEmpty(post.DescriptionImage))
				{
					string blogFileName = Path.GetFileName(post.DescriptionImage);
					string blogPath = Path.Combine(wwwRootPath, "BlogImages", blogFileName);

					if (System.IO.File.Exists(blogPath))
					{
						System.IO.File.Delete(blogPath);
					}
				}

			}

			var kategori = cm.GetById(id);
			cm.TDelete(kategori);
			return RedirectToAction("Index");

		}




	}
}
