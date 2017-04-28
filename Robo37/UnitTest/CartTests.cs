using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Domain.Abstract;
using WebUI.Controllers;
using System.Web.Mvc;
using WebUI.Models;

namespace UnitTest
{
    [TestClass]
    public class CartTests
    {
       // [TestMethod]
        public void Can_Add_New_Lines()
        {
            //Организация
            Course course1 = new Course { CourseId = 1, Name = "Course1" };
            Course course2 = new Course { CourseId = 2, Name = "Course2" };

            Cart cart = new Cart();

            //Действие
            cart.AddItem(course1, 1);
            cart.AddItem(course2, 2);
            List<CartLine> result = cart.Lines.ToList();

            //Утверждение
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Course, course1);
            Assert.AreEqual(result[1].Course, course2);
        }

        //[TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Организация
            Course course1 = new Course { CourseId = 1, Name = "Course1" };
            Course course2 = new Course { CourseId = 2, Name = "Course2" };

            Cart cart = new Cart();

            //Действие
            cart.AddItem(course1, 1);
            cart.AddItem(course2, 1);
            cart.AddItem(course1, 5);
            List<CartLine> result = cart.Lines.OrderBy(c => c.Course.CourseId).ToList();

            //Утверждение
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Quantity, 6);
            Assert.AreEqual(result[1].Quantity, 1);
        }

        //[TestMethod]
        public void Can_Remove_Line()
        {
            //Организация
            Course course1 = new Course { CourseId = 1, Name = "Course1" };
            Course course2 = new Course { CourseId = 2, Name = "Course2" };
            Course course3 = new Course { CourseId = 3, Name = "Course3" };

            Cart cart = new Cart();

            //Действие
            cart.AddItem(course1, 1);
            cart.AddItem(course2, 1);
            cart.AddItem(course1, 5);
            cart.AddItem(course3, 2);
            cart.RemoveLine(course2);

            //Утверждение
            Assert.AreEqual(cart.Lines.Where(c => c.Course == course2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        //[TestMethod]
        public void Calculate_Cart_Total()
        {
            //Организация
            Course course1 = new Course { CourseId = 1, Name = "Course1", Price = 100 };
            Course course2 = new Course { CourseId = 2, Name = "Course2", Price = 55 };

            Cart cart = new Cart();

            //Действие
            cart.AddItem(course1, 1);
            cart.AddItem(course2, 1);
            cart.AddItem(course1, 5);
            decimal result = cart.ComputeTotalValue();

            //Утверждение
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            //Организация
            Course course1 = new Course { CourseId = 1, Name = "Course1", Price = 100 };
            Course course2 = new Course { CourseId = 2, Name = "Course2", Price = 55 };

            Cart cart = new Cart();

            //Действие
            cart.AddItem(course1, 1);
            cart.AddItem(course2, 1);
            cart.AddItem(course1, 5);
            cart.Clear();

            //Утверждение
            Assert.AreEqual(cart.Lines.Count(), 0);
        }

        //Добавление элемента в корзину
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course> {
                new Course { CourseId = 1, Name = "Course1", Genre = "Genre1"}
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object, null);

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Course.CourseId, 1);
        }

        //После добавления книги в корзину - перенаправление на страницу корзины
        //[TestMethod]
        public void Adding_Course_To_Cart_Goes_To_Cart_Screen()
        {
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course> {
                new Course { CourseId = 1, Name = "Course1", Genre = "Genre1"}
            }.AsQueryable());

            Cart cart = new Cart();

            CartController controller = new CartController(mock.Object, null);

            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        //
        //[TestMethod]
        public void Can_View_Cart_Contents()
        {
            Cart cart = new Cart();
            CartController target = new CartController(null, null);

            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        //[TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();

            CartController controller = new CartController(null, mock.Object);

            ViewResult result = controller.Checkout(cart, shippingDetails);

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        //[TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Course(), 1);

            CartController controller = new CartController(null, mock.Object);
            controller.ModelState.AddModelError("error", "error");

            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_And_Submit_Order()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Cart cart = new Cart();
            cart.AddItem(new Course(), 1);

            CartController controller = new CartController(null, mock.Object);
            
            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());

            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
