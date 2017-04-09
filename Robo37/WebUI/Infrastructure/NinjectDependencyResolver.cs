using Domain.Abstract;
using Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            Mock<ICourseRepository> mock = new Mock<ICourseRepository>();
            mock.Setup(m => m.Courses).Returns(new List<Course>
            {
                new Course { Name = "Техноканикулы", Teacher = "Смирнов С.В.", Price = 7500 },
                new Course { Name = "Робоканикулы", Teacher = "Казарин А.С.", Price = 7500 },
                new Course { Name = "Робототехника. 1-ый уровень", Teacher = "Титов Д.С.", Price = 16000 }

            });
            kernel.Bind<ICourseRepository>().ToConstant(mock.Object);
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}