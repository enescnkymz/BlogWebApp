using DateAccessLayer.Abstract;
using DateAccessLayer.Concrete;
using DateAccessLayer.Models;
using DateAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;
using X.PagedList.Mvc.Core;
using X.PagedList.EF;

namespace DateAccessLayer.EntityFramework
{
	public class EfWriterRepository : GenericRepository<Writer>, IWriterDal
	{
		public async Task<IPagedList<AdminWriterListDto>> GetWritersWithStatisticsAsync(int pageNumber, int pageSize, string search)
		{
			using (var c = new Context())
			{
				var query = c.Writers.AsQueryable();

				// Filtreleme
				if (!string.IsNullOrWhiteSpace(search))
				{
					search = search.Trim().ToLower();

					// ID'ye göre arama (sayısal ise)
					if (int.TryParse(search, out int writerId))
					{
						query = query.Where(w => w.WriterID == writerId);
					}
					else
					{
						// İsme göre arama
						query = query.Where(w => w.WriterName.ToLower().Contains(search));
					}
				}


				var writers = query
					 .Select(w => new AdminWriterListDto
					 {
						 WriterId = w.WriterID,
						 Name = w.WriterName,
						 Email = w.WriterMail,
						 ImageUrl = w.WriterImage,
						 About = w.WriterAbout,
						 Status = w.WriterStatus,
						 BlogCount = w.Descriptions.Count(),
						 CommentCount = w.Comments.Count()
					 })
					.OrderByDescending(w => w.BlogCount);



				return await writers.ToPagedListAsync(pageNumber, pageSize);



			}


		}

		public int FindUserIdByMail(string mail) 
		{
			using (var c = new Context()) 
			{

				return c.Writers
				 .Where(x => x.WriterMail == mail)
				 .Select(x=>x.WriterID)
				 .FirstOrDefault();

			}
			
		}

		

	}
}
