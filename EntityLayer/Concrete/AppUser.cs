using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string? WriterImage { get; set; }
        public string? WriterAbout { get; set; }
        public int? WriterStatus { get; set; }

    }
}
