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
    public class UnitTestEmployeeLogic
    {
        [TestMethod]
        public async Task SaveEmployee_UpdatingValidEmployee_EmployeeUpdated()
        {
            var employeeData = new List<Employee>
            {
                new Employee
                {
                    id = 999,
                    name = "q",
                    SportType = new List<SportType> {new SportType {id = 1, name = "aaa"}},
                    Position = new Position {id = 1, name = "123123"}
                }
            }.AsQueryable();
            var sportTypeData = new List<SportType> {new SportType {id = 1, name = "aa"}}.AsQueryable();
            var positionData = new List<Position>
            {
                new Position
                {
                    id = 999,
                    name = "q"
                }
            }.AsQueryable();
            var employeeMockSet = new Mock<DbSet<Employee>>();
            employeeMockSet.As<IDbAsyncEnumerable<Employee>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Employee>(employeeData.GetEnumerator()));
            employeeMockSet.As<IQueryable<Employee>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Employee>(employeeData.Provider));
            employeeMockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employeeData.Expression);
            var sportTypeMockSet = new Mock<DbSet<SportType>>();
            sportTypeMockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(sportTypeData.GetEnumerator()));
            sportTypeMockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(sportTypeData.Provider));
            sportTypeMockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(sportTypeData.Expression);
            var positionMockSet = new Mock<DbSet<Position>>();
            positionMockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(positionData.GetEnumerator()));
            positionMockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(positionData.Provider));
            positionMockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(positionData.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Employee).Returns(employeeMockSet.Object);
            mockContext.Setup(m => m.SportType).Returns(sportTypeMockSet.Object);
            mockContext.Setup(m => m.Position).Returns(positionMockSet.Object);
            var sportTypeLogic = new EmployeeLogic {Context = mockContext.Object};
            var b = await sportTypeLogic.SaveEmployee(new DtoEmployee
            {
                Password = "asdsad",
                Id = 999,
                Name = "qqqqqqqqqqq",
                Position = new DtoPosition {Id = 999},
                SportTypes = new List<DtoSportType> {new DtoSportType {Id = 1}}
            });
            Assert.AreEqual(b, true);
            employeeMockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAllEmployees_ReturningAllEmployees_NullReturned()
        {
            var data = new List<Employee>().AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IDbAsyncEnumerable<Employee>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Employee>(data.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Employee>(data.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(emp => emp.Employee).Returns(mockSet.Object);
            var employeeLogic = new EmployeeLogic {Context = mockContext.Object};
            var e = await employeeLogic.GetEmployees();
            Assert.AreEqual(0, e.Count);
        }

        [TestMethod]
        public async Task GetEmployee_ReturningEmployeeWithSprecifiedId_NullReturned()
        {
            var data = new List<Employee>
            {
                new Employee {id = 1, name = "BBB"},
                new Employee {id = 2, name = "ZZZ"},
                new Employee {id = 3, name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IDbAsyncEnumerable<Employee>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Employee>(data.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Employee>(data.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(emp => emp.Employee).Returns(mockSet.Object);
            var employeeLogic = new EmployeeLogic {Context = mockContext.Object};
            var e = await employeeLogic.GetEmployee(4);
            Assert.IsNull(e);
        }

        [TestMethod]
        public async Task RemoveEmployee_RemovingEmployee_EmployeeRemoved()
        {
            var data = new List<Employee>
            {
                new Employee {id = 1, name = "BBB"},
                new Employee {id = 2, name = "ZZZ"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IDbAsyncEnumerable<Employee>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Employee>(data.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Employee>(data.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Employee).Returns(mockSet.Object);
            var employeeLogic = new EmployeeLogic {Context = mockContext.Object};
            var b = await employeeLogic.RemoveEmployee(1);
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Remove(It.IsAny<Employee>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task RemoveEmployee_RemovingEmployee_MethodFailed()
        {
            var data = new List<Employee>
            {
                new Employee {id = 1, name = "BBB"},
                new Employee {id = 2, name = "ZZZ"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IDbAsyncEnumerable<Employee>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Employee>(data.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Employee>(data.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Employee).Returns(mockSet.Object);
            var employeeLogic = new EmployeeLogic {Context = mockContext.Object};
            var b = await employeeLogic.RemoveEmployee(5);
            Assert.AreEqual(b, false);
            mockSet.Verify(m => m.Remove(It.IsAny<Employee>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
        }
    }
}