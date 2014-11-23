using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OrdersDb.Domain.Wrappers
{
    public interface IDbEntityEntry<TEntity> where TEntity : class
    {
        TEntity Entity { get; }
        EntityState State { get; set; }
        DbPropertyValues CurrentValues { get; }
        DbPropertyValues OriginalValues { get; }
        DbPropertyValues GetDatabaseValues();
        Task<DbPropertyValues> GetDatabaseValuesAsync();
        Task<DbPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken);
        void Reload();
        Task ReloadAsync();
        Task ReloadAsync(CancellationToken cancellationToken);
        DbReferenceEntry Reference(string navigationProperty);
        DbReferenceEntry<TEntity, TProperty> Reference<TProperty>(string navigationProperty) where TProperty : class;
        DbReferenceEntry<TEntity, TProperty> Reference<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty) where TProperty : class;
        DbCollectionEntry Collection(string navigationProperty);
        DbCollectionEntry<TEntity, TElement> Collection<TElement>(string navigationProperty) where TElement : class;

        DbCollectionEntry<TEntity, TElement> Collection<TElement>(Expression<Func<TEntity, ICollection<TElement>>> navigationProperty)
            where TElement : class;

        DbPropertyEntry Property(string propertyName);
        DbPropertyEntry<TEntity, TProperty> Property<TProperty>(string propertyName);
        DbPropertyEntry<TEntity, TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> property);
        DbComplexPropertyEntry ComplexProperty(string propertyName);
        DbComplexPropertyEntry<TEntity, TComplexProperty> ComplexProperty<TComplexProperty>(string propertyName);
        DbComplexPropertyEntry<TEntity, TComplexProperty> ComplexProperty<TComplexProperty>(Expression<Func<TEntity, TComplexProperty>> property);
        DbMemberEntry Member(string propertyName);
        DbMemberEntry<TEntity, TMember> Member<TMember>(string propertyName);
        DbEntityValidationResult GetValidationResult();
        bool Equals(DbEntityEntry<TEntity> other);
    }
}