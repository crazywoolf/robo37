using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        ICourseRepository repository;

        public AdminController(ICourseRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Courses);
        }
    }
}