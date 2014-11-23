using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;
using System.Data.Entity;

namespace OrdersDb.Domain.Services.Geography.Street
{
    public class StreetService : NamedServiceBase<Street, StreetSearchParameters, StreetDto>, IStreetService
    {
        public StreetService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override List<StreetDto> Search(StreetSearchParameters @params)
        {
            var query = Db.Set<Street>()
                .Include(x => x.City)
                .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (!string.IsNullOrEmpty(@params.CityName))
                query = query.Where(x => x.City.Name.ToLower().Contains(@params.CityName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new StreetDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  CityId = x.CityId,
                                                                  CityName = x.City != null ? x.City.Name : string.Empty
                                                              }).ToList();
        }

        public List<NameValue> GetStreetsByCity(int cityId)
        {
            return Db.Streets
                .Where(x => x.CityId == cityId)
                .OrderBy(x => x.City.Name)
                .Select(x => new NameValue { Id = x.Id, Name = x.Name })
                .ToList();
        }

        public override StreetDto GetById(int id)
        {
            var query = Db.Streets
                .Include(x => x.City.Region.Country)
                .AsQueryable();

            var streetDtos = new StreetDto();

            if (id != 0)
                streetDtos = query.Where(x => x.Id == id)
                     .Select(x => new StreetDto
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      CityId = x.CityId,
                                      CityName = x.City.Name
                                  }).Single();

            return streetDtos;
        }
    }
}
