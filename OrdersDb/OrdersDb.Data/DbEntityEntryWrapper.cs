using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Data
{
    public class DbEntityEntryWrapper<TEntity> : IDbEntityEntry<TEntity> where TEntity : class
    {
        private readonly DbEntityEntry<TEntity> EntityEntry;
        public DbPropertyValues GetDatabaseValues()
        {
            return EntityEntry.GetDatabaseValues();
        }

        public Task<DbPropertyValues> GetDatabaseValuesAsync()
        {
            return EntityEntry.GetDatabaseValuesAsync();
        }

        public Task<DbPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken)
        {
            return EntityEntry.GetDatabaseValuesAsync(cancellationToken);
        }

        public void Reload()
        {
            EntityEntry.Reload();
        }

        public Task ReloadAsync()
        {
            return EntityEntry.ReloadAsync();
        }

        public Task ReloadAsync(CancellationToken cancellationToken)
        {
            return EntityEntry.ReloadAsync(cancellationToken);
        }

        public DbReferenceEntry Reference(string navigationProperty)
        {
            return EntityEntry.Reference(navigationProperty);
        }

        public DbReferenceEntry<TEntity, TProperty> Reference<TProperty>(string navigationProperty) where TProperty : class
        {
            return EntityEntry.Reference<TProperty>(navigationProperty);
        }

        public DbReferenceEntry<TEntity, TProperty> Reference<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty) where TProperty : class
        {
            return EntityEntry.Reference(navigationProperty);
        }

        public DbCollectionEntry Collection(string navigationProperty)
        {
            return EntityEntry.Collection(navigationProperty);
        }

        public DbCollectionEntry<TEntity, TElement> Collection<TElement>(string navigationProperty) where TElement : class
        {
            return EntityEntry.Collection<TElement>(navigationProperty);
        }

        public DbCollectionEntry<TEntity, TElement> Collection<TElement>(Expression<Func<TEntity, ICollection<TElement>>> navigationProperty) where TElement : class
        {
            return EntityEntry.Collection(navigationProperty);
        }

        public DbPropertyEntry Property(string propertyName)
        {
            return EntityEntry.Property(propertyName);
        }

        public DbPropertyEntry<TEntity, TProperty> Property<TProperty>(string propertyName)
        {
            return EntityEntry.Property<TProperty>(propertyName);
        }

        public DbPropertyEntry<TEntity, TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            return EntityEntry.Property(property);
        }

        public DbComplexPropertyEntry ComplexProperty(string propertyName)
        {
            return EntityEntry.ComplexProperty(propertyName);
        }

        public DbComplexPropertyEntry<TEntity, TComplexProperty> ComplexProperty<TComplexProperty>(string propertyName)
        {
            return EntityEntry.ComplexProperty<TComplexProperty>(propertyName);
        }

        public DbComplexPropertyEntry<TEntity, TComplexProperty> ComplexProperty<TComplexProperty>(Expression<Func<TEntity, TComplexProperty>> property)
        {
            return EntityEntry.ComplexProperty(property);
        }

        public DbMemberEntry Member(string propertyName)
        {
            return EntityEntry.Member(propertyName);
        }

        public DbMemberEntry<TEntity, TMember> Member<TMember>(string propertyName)
        {
            return EntityEntry.Member<TMember>(propertyName);
        }

        public DbEntityValidationResult GetValidationResult()
        {
            return EntityEntry.GetValidationResult();
        }

        public bool Equals(DbEntityEntry<TEntity> other)
        {
            return EntityEntry.Equals(other);
        }

        public TEntity Entity
        {
            get { return EntityEntry.Entity; }
        }

        public EntityState State
        {
            get { return EntityEntry.State; }
            set { EntityEntry.State = value; }
        }

        public DbPropertyValues CurrentValues
        {
            get { return EntityEntry.CurrentValues; }
        }

        public DbPropertyValues OriginalValues
        {
            get { return EntityEntry.OriginalValues; }
        }

        public DbEntityEntryWrapper(DbEntityEntry<TEntity> entityEntry)
        {
            EntityEntry = entityEntry;
        }
    }
}