﻿using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUI.Controllers;

namespace UnitTest
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Courses()
        {
            // Организация (arrange)
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course>
            {
                new Course{ CourseId = 1, Name = "Course1"},
                new Course{ CourseId = 2, Name = "Course2"},
                new Course{ CourseId = 3, Name = "Course3"},
                new Course{ CourseId = 4, Name = "Course4"},
                new Course{ CourseId = 5, Name = "Course5"},
            });

            AdminController controller = new AdminController(mock.Object);
  
            // Действие (act)
            List<Course> result = ((IEnumerable<Course>)controller.Index().ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual(result[0].Name, "Course1");
            Assert.AreEqual(result[1].Name, "Course2");
        }
    }
}
