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
        // [TestMethod]
        public void Can_Paginate()
        {
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course>
            {
                new Course{ CourseId = 1, Name = "Course1"},
                new Course{ CourseId = 2, Name = "Course2"},
                new Course{ CourseId = 3, Name = "Course3"},
                new Course{ CourseId = 4, Name = "Course4"},
                new Course{ CourseId = 5, Name = "Course5"},
            });

            CoursesController controller = new CoursesController(mock.Object);
            controller.pageSize = 3;

            IEnumerable<Course> result = (IEnumerable<Course>)controller.List(2).Model;

            // Утверждение (assert)
            List<Course> courses = result.ToList();
            Assert.IsTrue(courses.Count == 2);
            Assert.AreEqual(courses[0].Name, "Course4");
            Assert.AreEqual(courses[1].Name, "Course5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Организация
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            
            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
    }
}
