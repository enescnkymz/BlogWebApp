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
                 return c.Descriptions.Include(x => x.Category).ToList();
			}
		}

		public Description GetDescriptionWithCategory(int id)
		{
			using (var c = new Context()) 
			{ 
				return c.Descriptions.Include(x => x.Category).Include(x => x.Writer).FirstOrDefault(x => x.DescriptionID == id); 
			}
		
		}
		
	}
}
