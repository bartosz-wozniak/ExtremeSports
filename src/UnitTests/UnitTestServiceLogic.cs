using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLogic;
using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shared.DtoObjects;

namespace UnitTests
{
    [TestClass]
    public class UnitTestServiceLogic
    {
        [TestMethod]
        public async Task AddService_AddingService_ServiceAdded()
        {
            var serviceData = new List<Service>().AsQueryable();
            var serviceMockSet = new Mock<DbSet<Service>>();
            serviceMockSet.As<IDbAsyncEnumerable<Service>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Service>(serviceData.GetEnumerator()));
            serviceMockSet.As<IQueryable<Service>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Service>(serviceData.Provider));
            serviceMockSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(serviceData.Expression);
            var serviceDateData = new List<ServiceDate>().AsQueryable();
            var serviceDateMockSet = new Mock<DbSet<ServiceDate>>();
            serviceDateMockSet.As<IDbAsyncEnumerable<ServiceDate>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<ServiceDate>(serviceDateData.GetEnumerator()));
            serviceDateMockSet.As<IQueryable<ServiceDate>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<ServiceDate>(serviceDateData.Provider));
            serviceDateMockSet.As<IQueryable<ServiceDate>>()
                .Setup(m => m.Expression)
                .Returns(serviceDateData.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Service).Returns(serviceMockSet.Object);
            mockContext.Setup(m => m.ServiceDate).Returns(serviceDateMockSet.Object);
            var serviceLogic = new ServiceLogic {Context = mockContext.Object};
            var b = await serviceLogic.AddService(new DtoService
            {
                InstructorId = 1,
                Dates = new List<DateTime> {new DateTime(1944, 05, 09)},
                ServiceTypeId = 1
            });
            Assert.AreEqual(b, true);
            serviceMockSet.Verify(m => m.Add(It.IsAny<Service>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task EnrolCourse_EnrolingCourse_CourseEnrolled()
        {
            var serviceList = new List<Service> {new Service {id = 1}};
            var serviceData = serviceList.AsQueryable();
            var serviceMockSet = new Mock<DbSet<Service>>();
            serviceMockSet.As<IDbAsyncEnumerable<Service>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Service>(serviceData.GetEnumerator()));
            serviceMockSet.As<IQueryable<Service>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Service>(serviceData.Provider));
            serviceMockSet.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(serviceData.Expression);
            var customerList = new List<Customer> {new Customer {id = 1}};
            var customerData = customerList.AsQueryable();
            var customerMockSet = new Mock<DbSet<Customer>>();
            customerMockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(customerData.GetEnumerator()));
            customerMockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(customerData.Provider));
            customerMockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerData.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Service).Returns(serviceMockSet.Object);
            mockContext.Setup(m => m.Customer).Returns(customerMockSet.Object);
            var serviceLogic = new ServiceLogic {Context = mockContext.Object};
            var b = await serviceLogic.EnrolCourse(1, 1);
            Assert.AreEqual(b, true);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }
    }
}