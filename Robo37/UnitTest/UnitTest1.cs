using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.HtmlHelpers;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {


        //[TestMethod]
        public void Can_Filter_Courses()
        {
            // Организация (arrange)
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course>
            {
                new Course{ CourseId = 1, Name = "Course1", Genre="Genre1"},
                new Course{ CourseId = 2, Name = "Course2", Genre="Genre2"},
                new Course{ CourseId = 3, Name = "Course3", Genre="Genre1"},
                new Course{ CourseId = 4, Name = "Course4", Genre="Genre3"},
                new Course{ CourseId = 5, Name = "Course5", Genre="Genre2"},
            });

            CoursesController controller = new CoursesController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            List<Course> result = ((CoursesListViewModel)controller.List("Genre2", 1).Model).Courses.ToList();
                      
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Course2" && result[0].Genre == "Genre2");
            Assert.IsTrue(result[1].Name == "Course5" && result[1].Genre == "Genre2");
        }

        //[TestMethod]
        public void Can_Create_Categories()
        {
            // Организация (arrange)
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course>
            {
                new Course{ CourseId = 1, Name = "Course1", Genre="Genre1"},
                new Course{ CourseId = 2, Name = "Course2", Genre="Genre2"},
                new Course{ CourseId = 3, Name = "Course3", Genre="Genre1"},
                new Course{ CourseId = 4, Name = "Course4", Genre="Genre3"},
                new Course{ CourseId = 5, Name = "Course5", Genre="Genre2"},
            });

            NavController target = new NavController(mock.Object);

            // Действие (act)
            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Genre1");
            Assert.AreEqual(result[1], "Genre2");
            Assert.AreEqual(result[2], "Genre3");

        }

        //[TestMethod]
        public void Can_Indicates_Selected_Genre()
        {
            // Организация (arrange)
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course>
            {
                new Course{ CourseId = 1, Name = "Course1", Genre="Genre1"},
                new Course{ CourseId = 2, Name = "Course2", Genre="Genre2"},
                new Course{ CourseId = 3, Name = "Course3", Genre="Genre1"},
                new Course{ CourseId = 4, Name = "Course4", Genre="Genre3"},
                new Course{ CourseId = 5, Name = "Course5", Genre="Genre2"},
            });

            NavController target = new NavController(mock.Object);

            string genreToSelect = "Genre2";

            // Действие (act)
            string result = target.Menu(genreToSelect).ViewBag.SelectedGenre;

            Assert.AreEqual(genreToSelect, result);


        }

        [TestMethod]
        public void Generete_Genre_Specific_Course_Count()
        {
            // Организация (arrange)
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course>
            {
                new Course{ CourseId = 1, Name = "Course1", Genre="Genre1"},
                new Course{ CourseId = 2, Name = "Course2", Genre="Genre2"},
                new Course{ CourseId = 3, Name = "Course3", Genre="Genre1"},
                new Course{ CourseId = 4, Name = "Course4", Genre="Genre3"},
                new Course{ CourseId = 5, Name = "Course5", Genre="Genre2"},
            });

            CoursesController controller = new CoursesController(mock.Object);
            controller.pageSize = 3;

            int res1 = ((CoursesListViewModel)controller.List("Genre1").Model).PagingInfo.TotalItems;
            int res2 = ((CoursesListViewModel)controller.List("Genre2").Model).PagingInfo.TotalItems;
            int res3 = ((CoursesListViewModel)controller.List("Genre3").Model).PagingInfo.TotalItems;
            int resAll = ((CoursesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
