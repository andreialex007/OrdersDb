using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersDb.Domain.Wrappers
{
    public interface IDbSet<TEntity> : IQueryable<TEntity> where TEntity : class
    {
        bool ContainsListCollection { get; }
        ObservableCollection<TEntity> Local { get; }
        IList GetList();
        IDbAsyncEnumerator GetAsyncEnumerator();
        DbQuery<TEntity> Include(string path);
        DbQuery<TEntity> AsNoTracking();
        TEntity Find(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<TEntity> FindAsync(params object[] keyValues);
        TEntity Attach(TEntity entity);
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
        TEntity Create();
        TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;
        DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters);
    }
}