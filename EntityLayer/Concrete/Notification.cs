using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{

	public enum NotificationTypeEnum
	{
		Message = 0,
		Task = 1,
		Warning = 2,
		Offer = 3,
		Like = 4,
		Upload = 5
	}


	public class Notification
	{

		[Key]
		public int NotificationID { get; set; }
		public NotificationTypeEnum NotificationType { get; set; }
		public string NotificationDetails { get; set; }
		public DateTime NotificationDate { get; set; }
		public bool NotificationStatus { get; set; }
		public bool IsRead { get; set; }
		public int? WriterID { get; set; }


	}
}
