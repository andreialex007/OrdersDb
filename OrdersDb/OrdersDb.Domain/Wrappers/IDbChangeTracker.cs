using System.Collections.Generic;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Data
{
    public interface IDbChangeTracker
    {
        IEnumerable<IDbEntityEntry<TEntity>> Entries<TEntity>() where TEntity : class;
        bool HasChanges();
        void DetectChanges();
    }
}