using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Description { get; set; }
        public string Platform { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; } 
    }
}
