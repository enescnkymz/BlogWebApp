using DateAccessLayer.Abstract;
using DateAccessLayer.Concrete;
using DateAccessLayer.Models;
using DateAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.EntityFramework
{
    public class EfCategoryRepository : GenericRepository<Category> , ICategoryDal
    {

		public List<CategoryBlogCountReadModel> GetBlogCountsByCategory()
		{
			using (var c = new Context()) 
			{
				
				var data = c.Categories.Select(x => new CategoryBlogCountReadModel
				{
					CategoryName = x.CategoryName,
					BlogCount = x.Descriptions.Count() 
				}).ToList();

				return data;
			}
		}


	}
}
