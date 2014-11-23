using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services.Geography.Region
{
    public class RegionService : NamedServiceBase<Region, RegionSearchParameters, RegionDto>, IRegionService
    {
        public RegionService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public void AddRegionWithCities(Region region)
        {
            //Валидирует сущность и дочерние сущности выбрасываем эксепшн если невалидно
            var errors = region.Validate(r => r.Cities);
            errors.ThrowIfHasErrors();

            //Аттачим сущность и дочерние города к контексту
            Db.AttachIfDetached(region,
                r => r.Cities);

            //Помечаем регион и дочерние города как добавленные
            Db.SetEntryState(region, EntityState.Added,
                r => r.Cities);

            //Сохраняем изменения
            Db.SaveChanges();
        }

        /// <summary>
        /// Возвращает регион с городами
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public List<Region> GetRegionsWithCities(int take, int skip)
        {
            return Db.Regions
                .Include(x => x.Cities)
                .OrderBy(x => x.Name)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public override RegionDto GetById(int id)
        {
            var query = Db.Regions
                    .Include(x => x.Country)
                    .AsQueryable();

            var region = new RegionDto();

            if (id != 0)
                region = query.Where(x => x.Id == id)
                     .Select(x => new RegionDto { Id = x.Id, Name = x.Name, CountryId = x.CountryId, CountryName = x.Country.Name })
                     .Single();

            region.Countries = Db.Countries
                .OrderBy(x => x.Name)
                .Select(x => new NameValue { Id = x.Id, Name = x.Name })
                .ToList();

            return region;
        }

        public List<NameValue> GetRegionsInCountry(int countryId)
        {
            return Db.Regions
                .Where(x => x.CountryId == countryId)
                .OrderBy(x => x.Name)
                .Select(x => new NameValue { Id = x.Id, Name = x.Name })
                .ToList();
        }

        public override List<RegionDto> Search(RegionSearchParameters @params)
        {
            var query = Db.Set<Region>()
                .Include(x => x.Country)
                .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (!string.IsNullOrEmpty(@params.CountryName))
                query = query.Where(x => x.Country.Name.ToLower().Contains(@params.CountryName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new RegionDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  CountryId = x.CountryId,
                                                                  CountryName = x.Country != null ? x.Country.Name : string.Empty
                                                              }).ToList();
        }
    }
}
