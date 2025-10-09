using EntityLayer.Concrete;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IDescriptionService
	{
		void AddDescription(Description description);
		void DeleteDescription(Description descriptiony);
		void UpdateDescription(Description description);
		List<Description> GetAllDescriptions();
		Description GetById(int id);
		List<Description> GetDescriptionsWithCategory();
		List<Description> GetPostsByID(int id);	
		Description GetDescriptionWithCategory(int id);
		List<Description> GetDescriptionsWithWriter(int id);
		List<Description> GetLast3Post();
	}
}
