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

        public void SaveCourse(Course course)
        {
            if (course.CourseId == 0)
            {
                context.Courses.Add(course);
            }
            else
            {
                Course dbEntry = context.Courses.Find(course.CourseId);
                if (dbEntry != null)
                {
                    dbEntry.Name = course.Name;
                    dbEntry.Teacher = course.Teacher;
                    dbEntry.Description = course.Description;
                    dbEntry.Genre = course.Genre;
                    dbEntry.Price = course.Price;
                }
            }
            context.SaveChanges();
        }
    }
}
