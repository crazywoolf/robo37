using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private ICourseRepository repository;

        public NavController(ICourseRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string genre = null)
        {
            ViewBag.SelectedGenre = genre;

            IEnumerable<string> genres = repository.Courses
                .Select(course => course.Genre)
                .Distinct()
                .OrderBy(x => x);
            
            return PartialView("FlexMenu", genres);
        }
    }
}