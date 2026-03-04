using BusinessLayer.Abstract;
using BusinessLayer.DTOs;
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
		private readonly IDescriptionDal _descriptionDal;

		public DescriptionManager(IDescriptionDal descriptionDal)
		{
			_descriptionDal = descriptionDal;
		}

		public List<Description> GetAll()
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

		public List<Description> GetDescriptionsByWriter(int id)
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

		public void TAdd(Description t)
		{
			_descriptionDal.İnsert(t);	
		}

		public void TDelete(Description t)
		{
			_descriptionDal.Delete(t);
		}

		public void TUpdate(Description t)
		{
			_descriptionDal.Update(t);
		}

		public List<Description> GetDescriptionsByWriterWithCategory(int id)
		{
			return _descriptionDal.GetBlogListWithCategoryByWriter(id);
		}

		public int GetBlogCountByWriter(int id)
		{
			return _descriptionDal.GetBlogCountByWriter(id);
		}

		public List<ExcelBlogDto> GetExcelBlogList()
		{
			
			var blogEntities = _descriptionDal.GetListAll();
		
			var blogsForExcel = blogEntities.Select(x => new ExcelBlogDto
			{
				BlogId = x.DescriptionID,
				BlogTitle = x.DescriptionTitle
			}).ToList();

			return blogsForExcel;
		}

		public List<Description> GetDescriptionsWithCommentCount()
		{
			return _descriptionDal.GetDescriptionsWithCommentCount();
		}

		public List<Description> WriterLast3Post(int id)
		{
			return _descriptionDal.WriterLast3Post(id);
		}

		public List<Description> GetDescriptionsByCategoryId(int id)
		{
			return _descriptionDal.GetDescriptionsByCategoryId(id);
		}
	}
}
