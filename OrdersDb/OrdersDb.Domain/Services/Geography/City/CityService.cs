using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services.Geography.City
{
    public class CityService : NamedServiceBase<City, CitySearchParameters, CityDto>, ICityService
    {
        public CityService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        /// <summary>
        /// Получает все города в регионе
        /// </summary>
        public List<City> GetCitiesInRegion(string regionName)
        {
            return Db.Cities
                .Include(x => x.Region)
                .Where(x => x.Region != null && x.Region.Name == regionName)
                .OrderBy(x => x.Name)
                .ToList();
        }

        public List<NameValue> GetCitiesInRegion(int regionId)
        {
            return Db.Cities
                .Include(x => x.Region)
                .Where(x => x.RegionId == regionId)
                .OrderBy(x => x.Name)
                .Select(x => new NameValue { Id = x.Id, Name = x.Name })
                .ToList();
        }

        /// <summary>
        /// Получает указанную страницу городов
        /// </summary>
        public List<City> GetCities(int take, int skip)
        {
            return Db.Cities
                 .OrderBy(x => x.Name)
                 .Take(take)
                 .Skip(skip)
                 .ToList();
        }

        public override List<CityDto> Search(CitySearchParameters @params)
        {
            var query = Db.Set<City>()
                .Include(x => x.Region)
                .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (@params.MinPopulation != null)
                query = query.Where(x => x.Population >= @params.MinPopulation);

            if (@params.MaxPopulation != null)
                query = query.Where(x => x.Population <= @params.MaxPopulation);

            if (!string.IsNullOrEmpty(@params.RegionName))
                query = query.Where(x => x.Region.Name.ToLower().Contains(@params.RegionName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new CityDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  Population = x.Population,
                                                                  RegionId = x.RegionId,
                                                                  RegionName = x.Region.Name
                                                              }).ToList();
        }

        public override CityDto GetById(int id)
        {
            return Db.Set<City>()
                .Include(x => x.Region)
                .Where(x => x.Id == id)
                .Select(x => new CityDto
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 Population = x.Population,
                                 RegionId = x.RegionId,
                                 RegionName = x.Region.Name
                             }).Single();
        }
    }
}
