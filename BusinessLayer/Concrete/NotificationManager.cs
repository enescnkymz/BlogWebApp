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
	public class NotificationManager : INotificationService
	{
		INotificationDal _notificationDal;

		public NotificationManager(INotificationDal notificationDal)
		{
			_notificationDal = notificationDal;
		}

		

		public List<Notification> GetAll()
		{
			return _notificationDal.GetListAll();
		}

		public List<Notification> GetAllByID(int id)
		{
			return _notificationDal.GetAllByID(id);
		}

		public Notification GetById(int id)
		{
			return _notificationDal.GetById(id);
		}

		public List<Notification> GetLast4NotificationByWriterID(int id)
		{
			return _notificationDal.GetLast4NotificationsByWriter(id);
		}

		public void TAdd(Notification t)
		{
			_notificationDal.İnsert(t);
		}

		public void TDelete(Notification t)
		{
			_notificationDal.Delete(t);
		}

		public void TUpdate(Notification t)
		{
			throw new NotImplementedException();
		}
	}
}
