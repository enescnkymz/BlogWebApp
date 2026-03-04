using BusinessLayer.DTOs;
using DateAccessLayer.Models;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BusinessLayer.Abstract
{
	public interface IWriterService : IGenericService<Writer>
	{
		UserNavbarDto GetUserNavbarInfoById(int id);
		Task<IPagedList<AdminWriterListDto>> GetWritersWithStatistics(int pageNumber, int pageSize, string search);
		int FindUserIdByMail(string mail);

	}
}
