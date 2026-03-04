using BusinessLayer.DTOs;
using EntityLayer.Concrete;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
	public interface IDescriptionService : IGenericService<Description>
	{		
		List<Description> GetDescriptionsWithCategory();
		List<Description> GetPostsByID(int id);	
		Description GetDescriptionWithCategory(int id);
		List<Description> GetDescriptionsByWriter(int id);
		List<Description> GetDescriptionsByWriterWithCategory(int id);
		List<Description> GetLast3Post();
		int GetBlogCountByWriter(int id);
		List<ExcelBlogDto> GetExcelBlogList();
		List<Description> GetDescriptionsWithCommentCount();
		List<Description> WriterLast3Post(int id);
		List<Description> GetDescriptionsByCategoryId(int id);
	}
}
