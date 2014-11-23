using System.Linq;
using Moq;

namespace OrdersDb.Domain.Tests
{
    public static class MockExtensions
    {
        public static void SetupIQueryable<T, TEntity>(this Mock<T> mock, IQueryable<TEntity> queryable)
            where T : class, IQueryable<TEntity>
        {
            mock.Setup(r => r.GetEnumerator()).Returns(queryable.GetEnumerator());
            mock.Setup(r => r.Provider).Returns(queryable.Provider);
            mock.Setup(r => r.ElementType).Returns(queryable.ElementType);
            mock.Setup(r => r.Expression).Returns(queryable.Expression);
        }
    }
}