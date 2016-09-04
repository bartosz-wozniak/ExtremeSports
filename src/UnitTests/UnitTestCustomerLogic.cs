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
    public class UnitTestCustomerLogic
    {
        [TestMethod]
        public async Task SaveCustomer_AddingValidCustomer_CustomerAdded()
        {
            var data = new List<Customer>().AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Customer).Returns(mockSet.Object);
            var customerLogic = new CustomerLogic {Context = mockContext.Object};
            var b = await customerLogic.SaveCustomer(new DtoCustomer());
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task SaveCustomer_UpdatingValidCustomer_CustomerUpdated()
        {
            var data = new List<Customer>
            {
                new Customer
                {
                    id = 999,
                    name = "q"
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Customer).Returns(mockSet.Object);
            var customerLogic = new CustomerLogic {Context = mockContext.Object};
            var b = await customerLogic.SaveCustomer(new DtoCustomer
            {Id = 999, Name = "qqqqqqqqqqq"});
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAllCustomers_ReturningAllCustomers_AllCustomersReturned()
        {
            var data = new List<Customer>
            {
                new Customer {name = "BBB"},
                new Customer {name = "ZZZ"},
                new Customer {name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.Customer).Returns(mockSet.Object);
            var customerLogic = new CustomerLogic {Context = mockContext.Object};
            var cust = await customerLogic.GetCustomers();
            Assert.AreEqual(3, cust.Count);
            Assert.AreEqual("BBB", cust[0].Name);
            Assert.AreEqual("ZZZ", cust[1].Name);
            Assert.AreEqual("AAA", cust[2].Name);
        }

        [TestMethod]
        public async Task GetAllCustomers_ReturningAllCustomers_NullReturned()
        {
            var data = new List<Customer>().AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.Customer).Returns(mockSet.Object);
            var customerLogic = new CustomerLogic {Context = mockContext.Object};
            var customers = await customerLogic.GetCustomers();
            Assert.AreEqual(0, customers.Count);
        }

        [TestMethod]
        public async Task GetCustomer_ReturningCustomerWithSprecifiedId_OneSpecifieCustomerReturned()
        {
            var data = new List<Customer>
            {
                new Customer {id = 1, name = "BBB"},
                new Customer {id = 2, name = "ZZZ"},
                new Customer {id = 3, name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.Customer).Returns(mockSet.Object);
            var customerLogic = new CustomerLogic {Context = mockContext.Object};
            var customer = await customerLogic.GetCustomer(1);
            Assert.AreEqual("BBB", customer.Name);
        }

        [TestMethod]
        public async Task GetCustomer_ReturningCustomerWithSprecifiedId_NullReturned()
        {
            var data = new List<Customer>
            {
                new Customer {id = 1, name = "BBB"},
                new Customer {id = 2, name = "ZZZ"},
                new Customer {id = 3, name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.Customer).Returns(mockSet.Object);
            var customerLogic = new CustomerLogic {Context = mockContext.Object};
            var customer = await customerLogic.GetCustomer(4);
            Assert.IsNull(customer);
        }

        [TestMethod]
        public async Task RemoveCustomer_RemovingCustomer_CustomerRemoved()
        {
            var data = new List<Customer>
            {
                new Customer {id = 1, name = "BBB"},
                new Customer {id = 2, name = "ZZZ"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Customer).Returns(mockSet.Object);
            var customerLogic = new CustomerLogic {Context = mockContext.Object};
            var b = await customerLogic.RemoveCustomer(1);
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Remove(It.IsAny<Customer>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task RemoveCustomer_RemovingCustomer_MethodFailed()
        {
            var data = new List<Customer>
            {
                new Customer {id = 1, name = "BBB"},
                new Customer {id = 2, name = "ZZZ"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IDbAsyncEnumerable<Customer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Customer>(data.GetEnumerator()));
            mockSet.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Customer>(data.Provider));
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Customer).Returns(mockSet.Object);
            var customerLogic = new CustomerLogic {Context = mockContext.Object};
            var b = await customerLogic.RemoveCustomer(5);
            Assert.AreEqual(b, false);
            mockSet.Verify(m => m.Remove(It.IsAny<Customer>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
        }
    }
}