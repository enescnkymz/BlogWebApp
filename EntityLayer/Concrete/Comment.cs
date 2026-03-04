using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public class Comment
	{
		[Key]
		public int CommentID { get; set; }
		public int? CommentSenderID { get; set; }
		public Writer CommentSender { get; set; }
		public string CommentContent { get; set; }
		public DateTime CommentDate { get; set; }
		public int BlogRate { get; set; }
		public bool CommentStatus { get; set; }
		public int? DescriptionID { get; set; }
		public Description Description { get; set; }
	}
}
