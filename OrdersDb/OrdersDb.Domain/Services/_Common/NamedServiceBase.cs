using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services._Common
{
    public interface INamedServiceBase<TEntity, TSearchParameters, TDto> : IServiceBase<TEntity, TSearchParameters, TDto>
        where TEntity : EntityBase, new()
        where TSearchParameters : SearchParameters
        where TDto : DtoBase, new()
    {
        List<NameValue> GetAllNameValues();
    }

    public class NamedServiceBase<TEntity, TSearchParameters, TDto> : ServiceBase<TEntity, TSearchParameters, TDto>,
        INamedServiceBase<TEntity, TSearchParameters, TDto>
        where TEntity : EntityBase, INamedEntity, new()
        where TSearchParameters : NamedSearchParameters
        where TDto : NamedDtoBase, new()
    {
        public NamedServiceBase(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override List<TDto> Search(TSearchParameters @params)
        {
            var query = Db.Set<TEntity>().AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            return query.OrderByTakeSkip(@params).Select(x => new TDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name
                                                              }).ToList();
        }

        protected static IQueryable<TEntity> SearchByName(IQueryable<TEntity> query, NamedSearchParameters @params)
        {
            if (!string.IsNullOrEmpty(@params.Name))
                query = query.Where(x => x.Name.ToLower().Contains(@params.Name.ToLower()));
            return query;
        }

        public List<NameValue> GetAllNameValues()
        {
            return Db.Set<TEntity>().Select(x => new NameValue
                                                 {
                                                     Id = x.Id,
                                                     Name = x.Name
                                                 }).ToList();
        }
    }
}
