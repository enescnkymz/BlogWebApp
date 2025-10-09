using BusinessLayer.Abstract;
using DateAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class DescriptionManager : IDescriptionService
	{
		IDescriptionDal _descriptionDal;

		public DescriptionManager(IDescriptionDal descriptionDal)
		{
			_descriptionDal = descriptionDal;
		}

		public void AddDescription(Description description)
		{
			throw new NotImplementedException();
		}

		public void DeleteDescription(Description descriptiony)
		{
			throw new NotImplementedException();
		}

		public List<Description> GetAllDescriptions()
		{
			return _descriptionDal.GetListAll();
		}

		public Description GetById(int id)
		{
			return _descriptionDal.GetById(id);
		}

		public List<Description> GetDescriptionsWithCategory()
		{
			return _descriptionDal.GetDescriptionsWithCategory();
		}

		public List<Description> GetDescriptionsWithWriter(int id)
		{
			return _descriptionDal.GetListAll(x=>x.WriterID==id);
		}

		public Description GetDescriptionWithCategory(int id)
		{ 
			return _descriptionDal.GetDescriptionWithCategory(id);
		}

		public List<Description> GetLast3Post()
		{
			return _descriptionDal.GetListAll().Take(3).ToList();	
		}

		public List<Description> GetPostsByID(int id)
		{
			return _descriptionDal.GetListAll(x=>x.DescriptionID==id);
		}

		public void UpdateDescription(Description description)
		{
			throw new NotImplementedException();
		}
	}
}
