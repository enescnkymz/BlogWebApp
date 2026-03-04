using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
	public enum UserRole
	{
		Admin = 1,
		Yazar = 2
	}

	public class Writer
    {
        [Key]
        public int WriterID { get; set; }
        public string? NameSurname { get; set; }
        public string? WriterName { get; set; }
        public string? WriterAbout { get; set; }
        public string? WriterImage { get; set; }
        public bool WriterStatus { get; set; }
        public string? WriterMail { get; set; }
        public string? WriterPassword { get; set; }
        public UserRole? UserRole { get; set; }
        public ICollection<Description> Descriptions { get; set; }
		public ICollection<Message> SenderMessages { get; set; }
		public ICollection<Message> ReceiverMessages { get; set; }
		public ICollection<Comment> Comments { get; set; }
        

	}
}
