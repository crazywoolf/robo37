using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CoursesController : Controller
    {
        private ICourseRepository repository;
        public int pageSize = 2;
        
        public CoursesController(ICourseRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int page = 1)
        {
            CoursesListViewModel model = new CoursesListViewModel
            {
                Courses = repository.Courses
                .OrderBy(course => course.CourseId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Courses.Count()
                }
            };

            return View(model);
        }
    }
}