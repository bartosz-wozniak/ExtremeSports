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
    public class UnitTestPositionLogic
    {
        [TestMethod]
        public async Task SavePosition_AddingValidPosition_PositionAdded()
        {
            var data = new List<Position>().AsQueryable();
            var mockSet = new Mock<DbSet<Position>>();
            mockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(data.GetEnumerator()));
            mockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(data.Provider));
            mockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(data.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Position).Returns(mockSet.Object);
            var positionLogic = new PositionLogic {Context = mockContext.Object};
            var b = await positionLogic.SavePosition(new DtoPosition());
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Add(It.IsAny<Position>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task SavePosition_UpdatingValidPosition_PositionUpdated()
        {
            var data = new List<Position>
            {
                new Position
                {
                    id = 999,
                    name = "q",
                    description = "a"
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Position>>();
            mockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(data.GetEnumerator()));
            mockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(data.Provider));
            mockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(data.Expression);
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Position).Returns(mockSet.Object);
            var positionLogic = new PositionLogic {Context = mockContext.Object};
            var b = await positionLogic.SavePosition(new DtoPosition
            {Id = 999, Name = "qqqqqqqqqqq", Description = "aaaa"});
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Add(It.IsAny<Position>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAllPositions_ReturningAllPositions_AllPositionsReturned()
        {
            var data = new List<Position>
            {
                new Position {name = "BBB"},
                new Position {name = "ZZZ"},
                new Position {name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Position>>();
            mockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(data.GetEnumerator()));
            mockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(data.Provider));
            mockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Position>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Position>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.Position).Returns(mockSet.Object);
            var positionLogic = new PositionLogic {Context = mockContext.Object};
            var pos = await positionLogic.GetAllPositions();
            Assert.AreEqual(3, pos.Count);
            Assert.AreEqual("BBB", pos[0].Name);
            Assert.AreEqual("ZZZ", pos[1].Name);
            Assert.AreEqual("AAA", pos[2].Name);
        }

        [TestMethod]
        public async Task GetAllPositions_ReturningAllPositions_NullReturned()
        {
            var data = new List<Position>().AsQueryable();
            var mockSet = new Mock<DbSet<Position>>();
            mockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(data.GetEnumerator()));
            mockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(data.Provider));
            mockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Position>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Position>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.Position).Returns(mockSet.Object);
            var positionLogic = new PositionLogic {Context = mockContext.Object};
            var pos = await positionLogic.GetAllPositions();
            Assert.AreEqual(0, pos.Count);
        }

        [TestMethod]
        public async Task GetPosition_ReturningPositionWithSprecifiedId_OneSpecifiedPositionReturned()
        {
            var data = new List<Position>
            {
                new Position {id = 1, name = "BBB"},
                new Position {id = 2, name = "ZZZ"},
                new Position {id = 3, name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Position>>();
            mockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(data.GetEnumerator()));
            mockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(data.Provider));
            mockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Position>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Position>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.Position).Returns(mockSet.Object);
            var positionLogic = new PositionLogic {Context = mockContext.Object};
            var pos = await positionLogic.GetPosition(1);
            Assert.AreEqual("BBB", pos.Name);
        }

        [TestMethod]
        public async Task GetPosition_ReturningPositionWithSprecifiedId_NullReturned()
        {
            var data = new List<Position>
            {
                new Position {id = 1, name = "BBB"},
                new Position {id = 2, name = "ZZZ"},
                new Position {id = 3, name = "AAA"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Position>>();
            mockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(data.GetEnumerator()));
            mockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(data.Provider));
            mockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Position>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Position>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(c => c.Position).Returns(mockSet.Object);
            var positionLogic = new PositionLogic {Context = mockContext.Object};
            var pos = await positionLogic.GetPosition(4);
            Assert.IsNull(pos);
        }

        [TestMethod]
        public async Task RemovePosition_RemovingPosition_PositionRemoved()
        {
            var data = new List<Position>
            {
                new Position {id = 1, name = "BBB"},
                new Position {id = 2, name = "ZZZ"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Position>>();
            mockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(data.GetEnumerator()));
            mockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(data.Provider));
            mockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Position>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Position>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Position).Returns(mockSet.Object);
            var positionLogic = new PositionLogic {Context = mockContext.Object};
            var b = await positionLogic.RemovePosition(1);
            Assert.AreEqual(b, true);
            mockSet.Verify(m => m.Remove(It.IsAny<Position>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [TestMethod]
        public async Task RemovePosition_RemovingPosition_MethodFailed()
        {
            var data = new List<Position>
            {
                new Position {id = 1, name = "BBB"},
                new Position {id = 2, name = "ZZZ"}
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Position>>();
            mockSet.As<IDbAsyncEnumerable<Position>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Position>(data.GetEnumerator()));
            mockSet.As<IQueryable<Position>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Position>(data.Provider));
            mockSet.As<IQueryable<Position>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Position>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Position>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ExtremeSportDBEntities>();
            mockContext.Setup(m => m.Position).Returns(mockSet.Object);
            var positionLogic = new PositionLogic {Context = mockContext.Object};
            var b = await positionLogic.RemovePosition(5);
            Assert.AreEqual(b, false);
            mockSet.Verify(m => m.Remove(It.IsAny<Position>()), Times.Never);
            mockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
        }
    }
}