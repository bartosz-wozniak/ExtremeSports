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
    public class UnitTestSportTypeLogic
    {
        [TestMethod]
        public async Task SaveSportType_AddingValidSportType_SportTypeAdded()
        {
            var data = new List<SportType>().AsQueryable();
            var mockSet = new Mock<DbSet<SportType>>();
            mockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(data.GetEnumerator()));
            mockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(data.Provider));
            mockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(data.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.SportType).Returns(mockSet.Object);
            var sportTypeLogic = new SportTypeLogic {Context = mockContext.Object};
            var b = await sportTypeLogic.SaveSportType(new DtoSportType());
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Add(It.IsAny<SportType>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task SaveSportType_UpdatingValidSportType_SportTypeUpdated()
        {
            var data = new List<SportType>
            {
                new SportType
                {
                    id = 999,
                    name = "q",
                    description = "a"
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<SportType>>();
            mockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(data.GetEnumerator()));
            mockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(data.Provider));
            mockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(data.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.SportType).Returns(mockSet.Object);
            var sportTypeLogic = new SportTypeLogic {Context = mockContext.Object};
            var b = await sportTypeLogic.SaveSportType(new DtoSportType
            {Id = 999, Name = "qqqqqqqqqqq", Description = "aaaa"});
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Add(It.IsAny<SportType>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAllSportTypes_ReturningAllSportTypes_AllSportTypesReturned()
        {
            var data = new List<SportType>
            {
                new SportType {name = "BBB"},
                new SportType {name = "ZZZ"},
                new SportType {name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<SportType>>();
            mockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(data.GetEnumerator()));
            mockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(data.Provider));
            mockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.SportType).Returns(mockSet.Object);
            var sportTypeLogic = new SportTypeLogic {Context = mockContext.Object};
            var sports = await sportTypeLogic.GetAllSportTypes();
            Assert.AreEqual(3, sports.Count);
            Assert.AreEqual("BBB", sports[0].Name);
            Assert.AreEqual("ZZZ", sports[1].Name);
            Assert.AreEqual("AAA", sports[2].Name);
        }

        [TestMethod]
        public async Task GetAllSportTypes_ReturningAllSportTypes_NullReturned()
        {
            var data = new List<SportType>().AsQueryable();
            var mockSet = new Mock<DbSet<SportType>>();
            mockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(data.GetEnumerator()));
            mockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(data.Provider));
            mockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.SportType).Returns(mockSet.Object);
            var sportTypeLogic = new SportTypeLogic {Context = mockContext.Object};
            var sports = await sportTypeLogic.GetAllSportTypes();
            Assert.AreEqual(0, sports.Count);
        }

        [TestMethod]
        public async Task GetSportType_ReturningSportTypeWithSprecifiedId_OneSpecifieSportTypeReturned()
        {
            var data = new List<SportType>
            {
                new SportType {id = 1, name = "BBB"},
                new SportType {id = 2, name = "ZZZ"},
                new SportType {id = 3, name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<SportType>>();
            mockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(data.GetEnumerator()));
            mockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(data.Provider));
            mockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.SportType).Returns(mockSet.Object);
            var sportTypeLogic = new SportTypeLogic {Context = mockContext.Object};
            var sports = await sportTypeLogic.GetSportType(1);
            Assert.AreEqual("BBB", sports.Name);
        }

        [TestMethod]
        public async Task GetSportType_ReturningSportTypeWithSprecifiedId_NullReturned()
        {
            var data = new List<SportType>
            {
                new SportType {id = 1, name = "BBB"},
                new SportType {id = 2, name = "ZZZ"},
                new SportType {id = 3, name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<SportType>>();
            mockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(data.GetEnumerator()));
            mockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(data.Provider));
            mockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.SportType).Returns(mockSet.Object);
            var sportTypeLogic = new SportTypeLogic {Context = mockContext.Object};
            var sports = await sportTypeLogic.GetSportType(4);
            Assert.IsNull(sports);
        }

        [TestMethod]
        public async Task RemoveSportType_RemovingSportType_SportTypeRemoved()
        {
            var data = new List<SportType>
            {
                new SportType {id = 1, name = "BBB"},
                new SportType {id = 2, name = "ZZZ"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<SportType>>();
            mockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(data.GetEnumerator()));
            mockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(data.Provider));
            mockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.SportType).Returns(mockSet.Object);
            var sportTypeLogic = new SportTypeLogic {Context = mockContext.Object};
            var b = await sportTypeLogic.RemoveSportType(1);
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Remove(It.IsAny<SportType>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task RemoveSportType_RemovingSportType_MethodFailed()
        {
            var data = new List<SportType>
            {
                new SportType {id = 1, name = "BBB"},
                new SportType {id = 2, name = "ZZZ"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<SportType>>();
            mockSet.As<IDbAsyncEnumerable<SportType>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<SportType>(data.GetEnumerator()));
            mockSet.As<IQueryable<SportType>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<SportType>(data.Provider));
            mockSet.As<IQueryable<SportType>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<SportType>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.SportType).Returns(mockSet.Object);
            var sportTypeLogic = new SportTypeLogic {Context = mockContext.Object};
            var b = await sportTypeLogic.RemoveSportType(5);
            Assert.AreEqual(b, false);
            mockSet.Verify(m => m.Remove(It.IsAny<SportType>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
        }
    }
}