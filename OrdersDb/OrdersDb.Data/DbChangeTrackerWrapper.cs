using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Data
{
    public class DbChangeTrackerWrapper : IDbChangeTracker
    {
        private readonly DbChangeTracker _dbChangeTracker;

        public IEnumerable<IDbEntityEntry<TEntity>> Entries<TEntity>() where TEntity : class
        {
            return _dbChangeTracker.Entries<TEntity>().Select(x => new DbEntityEntryWrapper<TEntity>(x));
        }

        public bool HasChanges()
        {
            return _dbChangeTracker.HasChanges();
        }

        public void DetectChanges()
        {
            _dbChangeTracker.DetectChanges();
        }

        public DbChangeTrackerWrapper(DbChangeTracker dbChangeTracker)
        {
            _dbChangeTracker = dbChangeTracker;
        }
    }
}
