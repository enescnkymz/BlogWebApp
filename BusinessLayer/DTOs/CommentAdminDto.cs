using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
	public class CommentAdminDto
	{
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public string CommentSenderName { get; set; }
        public DateTime CommentCreateDate { get; set; }
        public string CommentPost {  get; set; }
        public int CommentPostId { get; set; }
        public bool CommentStatus { get; set; }
        public int CommentSenderId { get; set; }


    }
}
