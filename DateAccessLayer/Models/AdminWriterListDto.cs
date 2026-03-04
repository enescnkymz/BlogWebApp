using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.Models
{
    public class AdminWriterListDto
    {
		public int WriterId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string ImageUrl { get; set; }
		public string About { get; set; }
		public bool Status { get; set; }
		public int BlogCount { get; set; }
		public int CommentCount { get; set; }

	}
}
