using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Utils
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Аттачит сущность и дочерние указанные сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="appDbContext">Контекст базы</param>
        /// <param name="entity">Родительская сущность для аттача</param>
        /// <param name="childEntitiesAction">Дочерние сущности для аттача</param>
        public static void AttachIfDetached<T>(this IAppDbContext appDbContext, T entity,
           params Expression<Func<T, IEnumerable<EntityBase>>>[] childEntitiesAction) where T : EntityBase
        {
            if (appDbContext.Entry<T>(entity).State == EntityState.Detached)
                appDbContext.Set<T>().Attach(entity);
            foreach (var action in childEntitiesAction)
            {
                action.Compile()
                    .Invoke(entity)
                    .ToList()
                    .ForEach(x => appDbContext.AttachIfDetached(x));
            }
        }


        public static void AttachAndModify<T>(this IAppDbContext appDbContext, T entity,
           params Expression<Func<T, IEnumerable<EntityBase>>>[] childEntitiesAction) where T : EntityBase
        {
            appDbContext.AttachIfDetached(entity, childEntitiesAction);
            appDbContext.Entry(entity).State = EntityState.Modified;
            appDbContext.Entry(entity).Property(x => x.Created).IsModified = false;
        }


        public static void AttachAndAdd<T>(this IAppDbContext appDbContext, T entity,
           params Expression<Func<T, IEnumerable<EntityBase>>>[] childEntitiesAction) where T : EntityBase
        {
            appDbContext.AttachIfDetached(entity, childEntitiesAction);
            appDbContext.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// Устанавливает стейт сущности и дочерних указанных
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="appDbContext">Контекст базы</param>
        /// <param name="entity">Родительская сущность для изменения состояния</param>
        /// <param name="state">стейт</param>
        /// <param name="childEntitiesAction"></param>
        public static void SetEntryState<T>(this IAppDbContext appDbContext, T entity,
            EntityState state,
            params Expression<Func<T, IEnumerable<EntityBase>>>[] childEntitiesAction) where T : EntityBase
        {
            appDbContext.Entry(entity).State = EntityState.Added;
            foreach (var action in childEntitiesAction)
            {
                action.Compile()
                    .Invoke(entity)
                    .ToList()
                    .ForEach(x => appDbContext.SetEntryState(x, state));
            }
        }

        public static IQueryable<T> IncludeAll<T>(this Wrappers.IDbSet<T> dbSet) where T : EntityBase
        {
            var names = typeof(T).GetProperties()
                .Select(x => new { Type = x.PropertyType, x.Name })
                .Where(x => typeof(EntityBase).IsAssignableFrom(x.Type))
                .Select(x => x.Name)
                .ToList();
            var query = dbSet.AsQueryable();
            names.ForEach(x =>
                          {
                              query = query.Include(x);
                          });
            return query;
        }

        public static void SetModifiedProperties<TEntity>(this IDbEntityEntry<TEntity> entityEntry,
            params Expression<Func<TEntity, object>>[] properties) where TEntity : EntityBase
        {
            foreach (var property in properties)
            {
                entityEntry.Property(property).IsModified = true;
            }
        }

    }
}
