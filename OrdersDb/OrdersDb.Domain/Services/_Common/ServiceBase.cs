using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Hosting;
using OrdersDb.Domain.Services.Geography.Country;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

// ReSharper disable PossibleInvalidOperationException

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

        protected HttpContext HttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

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

        public byte[] GetImage(Expression<Func<TEntity, byte[]>> propertyLambda, int? imageId)
        {
            return imageId.IsNullOrZero()
                ? GetImageTemporary(propertyLambda)
                : GetImageFromDb(propertyLambda, imageId.Value);
        }

        protected byte[] GetImageFromDb(Expression<Func<TEntity, byte[]>> propertyLambda, int id)
        {
            return Db.Set<TEntity>().Where(x => x.Id == id).Select(propertyLambda).Single();
        }

        protected byte[] GetImageTemporary(Expression<Func<TEntity, byte[]>> propertyLambda)
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetImagePath(propertyLambda))
                ? File.ReadAllBytes(HttpContext.Session.GetImagePath(propertyLambda))
                : null;
        }

        protected void UploadAndPrepareImageTemporary(Expression<Func<TEntity, byte[]>> propertyLambda ,byte[] imageData, int saveWidth = 100, int saveHeight = 100)
        {
            File.WriteAllBytes(HttpContext.Session.GetImagePath<TEntity>(propertyLambda), ImageUtils.ResizeAndConvertToJpg(imageData, saveWidth, saveHeight));
        }

        protected void UploadImageToDb(Expression<Func<TEntity, byte[]>> propertyLambda, byte[] imageData, int id)
        {
            var country = new TEntity { Id = id };
            Db.AttachIfDetached(country);
            Db.Entry(country).Property(propertyLambda).CurrentValue = imageData;
            Db.Entry(country).Property(propertyLambda).IsModified = true;
            HttpContext.Session.ClearImagePath(propertyLambda);
            Db.SaveChanges();
        }

        public void UploadImage(Expression<Func<TEntity, byte[]>> propertyLambda, byte[] imageData, int? countryId)
        {
            if (countryId.IsNullOrZero())
                UploadAndPrepareImageTemporary(propertyLambda, imageData);
            else
                UploadImageToDb(propertyLambda, imageData, countryId.Value);
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
