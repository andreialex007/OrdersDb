using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services._Common
{
    public interface IServiceBase<TEntity, TSearchParameters, TDto>
        where TEntity : EntityBase, new()
        where TSearchParameters : SearchParameters
        where TDto : DtoBase
    {
        /// <summary>
        /// Добавляет в базу новую сущность
        /// </summary>
        void Add(TEntity entity);

        /// <summary>
        /// Обновляет в базе имеющуюся сущность
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Удаляет сущность по id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        List<TDto> Search(TSearchParameters @params);
        int Total(TSearchParameters @params);
        TDto GetById(int id);
    }

    public interface IServiceBase<TEntity, TDto> : IServiceBase<TEntity, SearchParameters, TDto>
        where TEntity : EntityBase, new()
        where TDto : DtoBase
    {

    }
}