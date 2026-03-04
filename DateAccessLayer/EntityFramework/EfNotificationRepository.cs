using DateAccessLayer.Abstract;
using DateAccessLayer.Concrete;
using DateAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.EntityFramework
{
	public class EfNotificationRepository : GenericRepository<Notification> , INotificationDal
	{
		public List<Notification> GetAllByID(int id)
		{
			using (var c = new Context())
			{
				return c.Notifications					
					.Where(x => x.WriterID == id || x.WriterID == null)
					.OrderByDescending(x => x.NotificationDate)
					.ToList();
				
			}
		}

		public List<Notification> GetLast4NotificationsByWriter(int id)
		{
			using (var c = new Context())
			{
				return c.Notifications
					.Where(x => x.WriterID == id || x.WriterID == null)
					.OrderByDescending(x => x.NotificationDate)
					.Take(4)
					.ToList();
			}
		}
	}
}
