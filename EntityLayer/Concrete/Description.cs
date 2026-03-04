using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Description
    {
        [Key] 
        public int DescriptionID { get; set; }
        public string DescriptionTitle { get; set; }
        public string DescriptionContent  { get; set; }
        public string DescriptionImage { get; set; }
        public DateTime DescriptionCreateDate { get; set; }
        public int DescriptionStatus { get; set; }
        public int DescriptionViewCount { get; set; } = 0;
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public  List<Comment> Comments { get; set; }
        public int? WriterID { get; set; }
        public Writer Writer { get; set; }



    }
}
