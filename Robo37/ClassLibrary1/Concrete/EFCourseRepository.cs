using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFCourseRepository : ICourseRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Course> Courses
        {
            get { return context.Courses; }
        }
    }
}
