using Microsoft.EntityFrameworkCore;
using Moq;

namespace Testes
{
    public static class DbSetMock
    {
        public static Mock<DbSet<T>> CreateFrom<T>(List<T> list) where T : class
        {
            var internalQueryable = list.AsQueryable();
            var mock = new Mock<DbSet<T>>();
            mock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(internalQueryable.Provider);
            mock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(internalQueryable.Expression);
            mock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(internalQueryable.ElementType);
            mock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => internalQueryable.GetEnumerator());

        
            return mock;
        }
    }
}