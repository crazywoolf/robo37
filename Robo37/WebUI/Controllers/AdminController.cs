using Domain.Abstract;
using Domain.Entities;
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

        public ViewResult Edit(int courseId)
        {
            Course course = repository.Courses.FirstOrDefault(b => b.CourseId == courseId);

            return View(course);
        }
        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCourse(course);
                TempData["message"] = string.Format("Изменения информации о курсе \"{0}\" сохранены", course.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(course);
            }
        }
    }
}