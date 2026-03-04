using DateAccessLayer.Abstract;
using DateAccessLayer.Concrete;
using DateAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.EntityFramework
{
	public class EfDescriptionRepository : GenericRepository<Description>, IDescriptionDal
	{
		public List<Description> GetDescriptionsWithCategory()
		{
			using (var c = new Context())
			{
                 return c.Descriptions.Include(x => x.Category).OrderByDescending(x => x.DescriptionCreateDate).ToList();
			}
		}

		public Description GetDescriptionWithCategory(int id)
		{
			using (var c = new Context()) 
			{ 
				return c.Descriptions
					.Include(x => x.Category)
					.Include(x => x.Writer)
					.FirstOrDefault(x => x.DescriptionID == id); 
			}
		
		}
		public List<Description> GetBlogListWithCategoryByWriter(int id)
		{
			using (var c = new Context())
			{
				return c.Descriptions
					.Include(x => x.Category)  
					.Where(x => x.WriterID == id)
					.OrderByDescending(x=>x.DescriptionCreateDate)             
					.ToList();
			}
		}
		public int GetBlogCountByWriter(int WriterID)
		{
			using (var c = new Context())
			{
				return c.Descriptions.Count(x => x.WriterID == WriterID);
			}
		}

		public List<Description> GetDescriptionsWithCommentCount()
		{
			using (var c = new Context())
			{

				DateTime lastWeek = DateTime.Now.AddDays(-7);

				return c.Descriptions
					.Include(x=>x.Comments)
					.OrderByDescending(x=>x.Comments.Count())
					.Where(x => x.DescriptionCreateDate >= lastWeek)
					.Take(3)
					.ToList();

			}
		}

		public List<Description> WriterLast3Post(int id)
		{
			using (var c = new Context()) 
			{
				return c.Descriptions
					.Where(x=>x.WriterID == id)
					.OrderByDescending(x=>x.DescriptionCreateDate)
					.Take(3)
					.ToList();

			}
		}
		public List<Description> GetDescriptionsByCategoryId(int id)
		{
			using (var c = new Context())
			{
				return c.Descriptions
					.Where(x => x.CategoryID == id)
					.ToList();

			}
		}





	}
}
