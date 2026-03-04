using DateAccessLayer.Models;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace DateAccessLayer.Abstract
{
    public interface IWriterDal : IGenericDal<Writer>
    {
		Task<IPagedList<AdminWriterListDto>> GetWritersWithStatisticsAsync(int pageNumber, int pageSize, string search);
		int FindUserIdByMail(string mail);
		
	}
}
