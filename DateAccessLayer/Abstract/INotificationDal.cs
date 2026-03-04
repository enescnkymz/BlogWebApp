using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.Abstract
{
	public interface INotificationDal:IGenericDal<Notification>
	{
		List<Notification> GetLast4NotificationsByWriter(int id);
		List<Notification> GetAllByID(int id);
	}
}
