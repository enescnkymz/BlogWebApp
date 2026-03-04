using DateAccessLayer.Abstract;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using DateAccessLayer.EntityFramework;
using X.PagedList;
using DateAccessLayer.Models;

namespace BusinessLayer.Concrete
{
	public class WriterManager : IWriterService
	{
		IWriterDal _writerDal;
		public WriterManager(IWriterDal writerDal) 
		{ 
			_writerDal = writerDal;
		}	

		public List<Writer> GetAll()
		{
			return _writerDal.GetListAll();
		}

		public Writer GetById(int id)
		{
			return _writerDal.GetById(id);
		}

		public UserNavbarDto GetUserNavbarInfoById(int id)
		{
			var user = _writerDal.GetById(id);

			return new UserNavbarDto
			{
				UserName = user.WriterName,
				UserImage = user.WriterImage
			};
		}

		public void TAdd(Writer t)
		{
			_writerDal.İnsert(t);
		}

		public void TDelete(Writer t)
		{
			_writerDal.Delete(t);
		}

		public void TUpdate(Writer t)
		{
			_writerDal.Update(t);
		}

		public async Task<IPagedList<AdminWriterListDto>> GetWritersWithStatistics(int pageNumber, int pageSize, string search)
		{
			return await _writerDal.GetWritersWithStatisticsAsync(pageNumber, pageSize, search);
		}

		public int FindUserIdByMail(string mail)
		{
			 return _writerDal.FindUserIdByMail(mail);
		}
	}
}
