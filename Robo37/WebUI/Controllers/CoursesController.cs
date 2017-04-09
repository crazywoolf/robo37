using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class CoursesController : Controller
    {
        private ICourseRepository repository;
        public CoursesController(ICourseRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Courses);
        }
    }
}