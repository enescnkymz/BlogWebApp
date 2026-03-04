using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Helpers
{
	public static class DateHelpers
	{
		public static string TimeAgo(DateTime dateTime)
		{
			TimeSpan timeSince = DateTime.Now - dateTime;

			if (timeSince.TotalSeconds < 1)
			{
				return "az önce";
			}

			if (timeSince.TotalMinutes < 1)
			{
				return (int)timeSince.TotalSeconds + " saniye önce";
			}
			if (timeSince.TotalHours < 1)
			{
				return (int)timeSince.TotalMinutes + " dakika önce";
			}
			if (timeSince.TotalDays < 1)
			{
				return (int)timeSince.TotalHours + " saat önce";
			}
			if (timeSince.TotalDays < 7)
			{
				return (int)timeSince.TotalDays + " gün önce";
			}
			if (timeSince.TotalDays < 30)
			{
				int haftalar = (int)(timeSince.TotalDays / 7);
				return haftalar + " hafta önce";
			}
			if (timeSince.TotalDays < 365)
			{
				int aylar = (int)(timeSince.TotalDays / 30);
				return aylar + " ay önce";
			}

			return dateTime.ToString("dd MMMM yyyy"); 
		}
	}
}
