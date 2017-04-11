using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class CoursesListViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}