using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

// ReSharper disable PossibleInvalidOperationException

namespace OrdersDb.Domain.Services.Geography.Country
{
    public class CountryService :
        NamedServiceBase<Country, CountrySearchParameters, CountryDto>,
        ICountryService
    {
        private readonly Size CountryImageFlagSize = new Size(100, 100);

        public CountryService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override List<CountryDto> Search(CountrySearchParameters @params)
        {
            HttpContext.Session.ClearImagePath<Country>(x => x.Flag); //TODO убрать костыль

            var query = Db.Set<Country>()
                .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (!string.IsNullOrEmpty(@params.Code))
                query = query.Where(x => x.Code.ToLower().Contains(@params.Code.ToLower()));

            if (!string.IsNullOrEmpty(@params.RussianName))
                query = query.Where(x => x.RussianName.ToLower().Contains(@params.RussianName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new CountryDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  Code = x.Code,
                                                                  RussianName = x.RussianName,
                                                                  Flag = x.Flag
                                                              }).ToList();
        }

        public override CountryDto GetById(int id)
        {
            HttpContext.Session.ClearImagePath<Country>(x => x.Flag);

            var query = Db.Countries.
                AsQueryable();

            var countryDto = query.Where(x => x.Id == id)
                .Select(x => new CountryDto
                             {
                                 Id = x.Id,
                                 Name = x.Name,
                                 Code = x.Code,
                                 Flag = x.Flag,
                                 RussianName = x.Name
                             })
                .Single();

            return countryDto;
        }

        public override void Add(Country entity)
        {
            entity.Flag = File.ReadAllBytes(HttpContext.Session.GetImagePath<Country>(x => x.Flag));
            Validate(entity);
            Db.AttachIfDetached(entity);
            Db.Entry(entity).State = EntityState.Added;
            Db.SaveChanges();
            HttpContext.Session.ClearImagePath<Country>(x => x.Flag);
        }


        public override void Update(Country entity)
        {
            var dbCountry = Db.Set<Country>().Single(x => x.Id == entity.Id);
            dbCountry.Name = entity.Name;
            dbCountry.RussianName = entity.RussianName;
            dbCountry.Code = entity.Code;
            Validate(dbCountry);
            Db.SaveChanges();
            HttpContext.Session.ClearImagePath<Country>(x => x.Flag);
        }

        protected override void Validate(Country entity)
        {
            var errors = entity.GetValidationErrors().ToList();
            if (entity.Flag == null)
                errors.Add(DbValidation.ErrorFor<Country>(x => x.Flag, "Flag image required"));
            errors.ThrowIfHasErrors();
        }
    }
}
