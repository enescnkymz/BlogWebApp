using BusinessLayer.Abstract;
using BusinessLayer.DTOs;
using DateAccessLayer.Abstract;
using DateAccessLayer.EntityFramework;
using DateAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
      
        
		public List<Category> GetAll()
		{
			return _categoryDal.GetListAll();
		}
		
        public Category GetById(int id)
        {
            return _categoryDal.GetById(id);
        }

		public void TAdd(Category t)
		{
			_categoryDal.İnsert(t);
		}

		public void TDelete(Category t)
		{
			_categoryDal.Delete(t);
		}

		public void TUpdate(Category t)
		{
			_categoryDal.Update(t);
		}

		public List<CategoryChartDto> GetCategoryChart()
		{
			var data = _categoryDal.GetBlogCountsByCategory();

			var values = data.Select(x => new CategoryChartDto
			{
				CategoryName = x.CategoryName,
				BlogCount = x.BlogCount,

			}).ToList();
			
			return values;
			
		} 
		
    }
}
