using System.Collections.Generic;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Wrappers;
using System.Data.Entity;
using System.Linq;
using OrdersDb.Domain.Utils;

namespace OrdersDb.Domain.Services.Geography.Hose
{
    public class HouseService : ServiceBase<House, HouseSearchParameters, HouseDto>, IHouseService
    {
        public HouseService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override List<HouseDto> Search(HouseSearchParameters @params)
        {
            var query = Db.Set<House>()
                .Include(x => x.Street)
                .AsQueryable();

            query = SearchByIds(query, @params);

            if (@params.Numbers != null)
                query = query.Where(x => @params.Numbers.Contains(x.Number));

            if (!string.IsNullOrEmpty(@params.Building))
                query = query.Where(x => x.Building.ToLower().Contains(@params.Building.ToLower()));

            if (!string.IsNullOrEmpty(@params.PostalCode))
                query = query.Where(x => x.PostalCode.ToLower().Contains(@params.PostalCode.ToLower()));

            if (!string.IsNullOrEmpty(@params.StreetName))
                query = query.Where(x => x.Street.Name.ToLower().Contains(@params.StreetName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new HouseDto
                                                              {
                                                                  Id = x.Id,
                                                                  Number = x.Number,
                                                                  Building = x.Building,
                                                                  PostalCode = x.PostalCode,
                                                                  StreetId = x.StreetId,
                                                                  StreetName = x.Street != null ? x.Street.Name : string.Empty
                                                              }).ToList();
        }

        public override HouseDto GetById(int id)
        {
            var query = Db.Houses.Include(x => x.Street.City.Region.Country).AsQueryable();

            var entity = new HouseDto();

            if (id != 0)
                entity = query.Where(x => x.Id == id)
                    .Select(x => new HouseDto
                                 {
                                     Id = x.Id,
                                     Building = x.Building,
                                     Number = x.Number,
                                     PostalCode = x.PostalCode,
                                     StreetId = x.StreetId,
                                     StreetName = x.Street.Name
                                 }).Single();

            return entity;
        }
    }
}
