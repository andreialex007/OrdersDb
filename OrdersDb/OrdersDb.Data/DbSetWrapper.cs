using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersDb.Data
{
    public class DbSetWrapper<TEntity> : Domain.Wrappers.IDbSet<TEntity> where TEntity : class
    {
        public readonly DbSet<TEntity> _dbSet;

        public DbSetWrapper(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        public Expression Expression
        {
            get { return ((IQueryable)_dbSet).Expression; }
        }

        public Type ElementType
        {
            get { return ((IQueryable)_dbSet).ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return ((IQueryable)_dbSet).Provider; }
        }

        public IList GetList()
        {
            return ((IListSource)_dbSet).GetList();
        }

        public bool ContainsListCollection
        {
            get { return ((IListSource)_dbSet).ContainsListCollection; }
        }

        public IDbAsyncEnumerator GetAsyncEnumerator()
        {
            return ((IDbAsyncEnumerable)_dbSet).GetAsyncEnumerator();
        }

        public DbQuery<TEntity> Include(string path)
        {
            return _dbSet.Include(path);
        }

        public DbQuery<TEntity> AsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return _dbSet.FindAsync(cancellationToken, keyValues);
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }

        public TEntity Attach(TEntity entity)
        {
            return _dbSet.Attach(entity);
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return _dbSet.AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            return _dbSet.RemoveRange(entities);
        }

        public TEntity Create()
        {
            return _dbSet.Create();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return _dbSet.Create<TDerivedEntity>();
        }

        public DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return _dbSet.SqlQuery(sql, parameters);
        }

        public ObservableCollection<TEntity> Local
        {
            get { return _dbSet.Local; }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)_dbSet).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}