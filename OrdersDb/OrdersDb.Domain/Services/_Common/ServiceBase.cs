using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services._Common
{
    /// <summary>
    /// Сервис предоставляющий базовую crud функциональность
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TSearchParameters"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public class ServiceBase<TEntity, TSearchParameters, TDto> : ServiceBase, IServiceBase<TEntity, TSearchParameters, TDto>
        where TEntity : EntityBase, new()
        where TSearchParameters : SearchParameters
        where TDto : DtoBase, new()
    {
        protected readonly IAppDbContext Db;
        protected readonly IObjectContext ObjectContext;

        public ServiceBase(IAppDbContext db, IObjectContext objectContext)
        {
            Db = db;
            ObjectContext = objectContext;
            ObjectContext.SavingChanges += OnSavingChanges;
        }

        /// <summary>
        /// Добавляет в базу новую сущность
        /// </summary>
        public virtual void Add(TEntity entity)
        {
            Validate(entity);
            Db.AttachIfDetached(entity);
            Db.Entry(entity).State = EntityState.Added;
            Db.SaveChanges();
        }


        /// <summary>
        /// Обновляет в базе имеющуюся сущность
        /// </summary>
        public virtual void Update(TEntity entity)
        {
            Validate(entity);
            Db.AttachIfDetached(entity);
            Db.Entry(entity).State = EntityState.Modified;
            Db.Entry(entity).Property(x => x.Created).IsModified = false;
            Db.SaveChanges();
        }

        /// <summary>
        /// Удаляет сущность по id
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            var c = new TEntity { Id = id };
            Db.Entry(c).State = EntityState.Deleted;
            Db.SaveChanges();
        }

        public virtual TDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Выполняет поиск по параметрам в базе, выводит указанную страницу
        /// </summary>
        /// <param name="params">Параметры для поиска</param>
        /// <returns></returns>
        public virtual List<TDto> Search(TSearchParameters @params)
        {
            var query = Db.Set<TEntity>().AsQueryable();
            query = SearchByIds(query, @params);
            return query.OrderByTakeSkip(@params).Select(x => new TDto { Id = x.Id }).ToList();
        }

        public virtual int Total(TSearchParameters @params)
        {
            return SearchByIds(Db.Set<TEntity>().AsQueryable(), @params).Count();
        }

        protected static IQueryable<TEntity> SearchByIds(IQueryable<TEntity> query, SearchParameters @params)
        {
            if (!@params.Ids.IsNullOrEmpty())
                query = query.Where(x => @params.Ids.Contains(x.Id));
            return query;
        }

        protected virtual void Validate(TEntity entity)
        {
            var errors = entity.GetValidationErrors();
            errors.ThrowIfHasErrors();
        }

        protected virtual void OnSavingChanges(object sender, EventArgs eventArgs)
        {
            var entries = Db.ChangeTracker.Entries<EntityBase>().ToList();
            entries.Where(x => x.State == EntityState.Modified).ToList().ForEach(x => x.Entity.Modified = DateTime.Now);
            entries.Where(x => x.State == EntityState.Added).ToList().ForEach(x =>
                                                                              {
                                                                                  x.Entity.Created = DateTime.Now;
                                                                                  x.Entity.Modified = DateTime.Now;
                                                                              });
        }
    }

    /// <summary>
    /// Сервис предоставляющий базовую crud функциональность
    /// </summary>
    public class ServiceBase<TEntity, TDto> : ServiceBase<TEntity, SearchParameters, TDto>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
    {
        public ServiceBase(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }
    }

    /// <summary>
    /// Сервис предоставляющий базовую crud функциональность
    /// </summary>
    public class ServiceBase
    {

    }
}
