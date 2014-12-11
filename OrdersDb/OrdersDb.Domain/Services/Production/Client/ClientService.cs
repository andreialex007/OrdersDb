using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using OrdersDb.Domain.Services.Geography.City;
using OrdersDb.Domain.Services.Geography.Country;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services.Geography.Region;
using OrdersDb.Domain.Services.Geography.Street;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Wrappers;
using System.Data.Entity;
using System.Web.UI.WebControls;
using OrdersDb.Domain.Utils;

namespace OrdersDb.Domain.Services.Production.Client
{
    public class ClientService : NamedServiceBase<Client, ClientSearchParameters, ClientDto>, IClientService
    {
        public ClientService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override List<ClientDto> Search(ClientSearchParameters @params)
        {
            var query = Db.Set<Client>()
                .Include(x => x.Location.Street.City.Region.Country)
                .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (!string.IsNullOrEmpty(@params.FullName))
                query = query.Where(x => x.FullName.ToLower().Contains(@params.FullName.ToLower()));

            if (!string.IsNullOrEmpty(@params.INN))
                query = query.Where(x => x.INN.ToLower().Contains(@params.INN.ToLower()));

            if (!string.IsNullOrEmpty(@params.OGRN))
                query = query.Where(x => x.OGRN.ToLower().Contains(@params.OGRN.ToLower()));

            if (!string.IsNullOrEmpty(@params.LocationString))
            {
                query = query.Where(x =>
                    x.Location.Street.Name.ToLower().Contains(@params.LocationString) ||
                    x.Location.Street.City.Name.ToLower().Contains(@params.LocationString) ||
                    x.Location.Street.City.Region.Name.ToLower().Contains(@params.LocationString) ||
                    x.Location.Street.City.Region.Country.Name.ToLower().Contains(@params.LocationString)
                    );
            }

            var result = query.OrderByTakeSkip(@params).Select(x => new ClientDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  FullName = x.FullName,
                                                                  INN = x.INN,
                                                                  OGRN = x.OGRN,
                                                                  CountryName = x.Location.Street.City.Region.Country.Name,
                                                                  RegionName = x.Location.Street.City.Region.Name,
                                                                  CityName = x.Location.Street.City.Name,
                                                                  StreetName = x.Location.Street.Name,
                                                                  Location = new HouseDto
                                                                             {
                                                                                 Id = x.Location.Id,
                                                                                 Building = x.Location.Building,
                                                                                 Number = x.Location.Number,
                                                                                 PostalCode = x.Location.PostalCode,
                                                                                 StreetId = x.Location.StreetId,
                                                                                 StreetName = x.Location.Street.Name
                                                                             }
                                                              }).ToList();

            return result;
        }

        public override void Update(Client entity)
        {
            Validate(entity);
            Db.AttachAndModify(entity);
            Db.AttachAndModify(entity.Location);
            Db.SaveChanges();
        }

        public override void Add(Client entity)
        {
            Validate(entity);
            Db.AttachAndAdd(entity.Location);
            Db.AttachAndAdd(entity);
            Db.SaveChanges();
        }

        protected override void Validate(Client entity)
        {
            //TODO: remove magic strings
            var errors = new List<DbValidationError>();
            errors.AddRange(entity.GetValidationErrors());
            errors.AddRange(entity.Location.GetValidationErrors(x => x.StreetId).OfProperty("Street"));
            errors.AddRange(entity.Location.Street.GetValidationErrors(x => x.CityId).OfProperty("City"));
            errors.AddRange(entity.Location.Street.City.GetValidationErrors(x => x.RegionId).OfProperty("Region"));
            errors.AddRange(entity.Location.Street.City.Region.GetValidationErrors(x => x.CountryId).OfProperty("Country"));
            errors.ThrowIfHasErrors();
        }

        public override ClientDto GetById(int id)
        {
            var clients = Db.Clients
                .Include(x => x.Location.Street.City.Region.Country)
                .AsQueryable();

            var countriesNameValues = Db.Countries.Select(c => new NameValue { Id = c.Id, Name = c.Name }).ToList();

            var client = new ClientDto();

            if (id != 0)
                client = clients.Where(x => x.Id == id)
                       .Select(x => new ClientDto
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        FullName = x.FullName,
                                        INN = x.INN,
                                        OGRN = x.OGRN,
                                        CityId = x.Location.Street.CityId,
                                        CityName = x.Location.Street.City.Name,
                                        CountryName = x.Location.Street.City.Region.Country.Name,
                                        CountryId = x.Location.Street.City.Region.Country.Id,
                                        RegionName = x.Location.Street.City.Region.Name,
                                        RegionId = x.Location.Street.City.Region.Id,
                                        StreetName = x.Location.Street.Name,
                                        StreetId = x.Location.Street.Id,
                                        Cities = x.Location.Street.City.Region.Cities.OrderBy(c => c.Name).Select(c => new NameValue { Id = c.Id, Name = c.Name }).ToList(),
                                        Regions = x.Location.Street.City.Region.Country.Regions.OrderBy(c => c.Name).Select(c => new NameValue { Id = c.Id, Name = c.Name }).ToList(),
                                        Streets = x.Location.Street.City.Streets.OrderBy(c => c.Name).Select(c => new NameValue { Id = c.Id, Name = c.Name }).ToList(),
                                        Location = new HouseDto
                                                   {
                                                       Id = x.Location.Id,
                                                       Building = x.Location.Building,
                                                       Number = x.Location.Number,
                                                       PostalCode = x.Location.PostalCode
                                                   }
                                    }).Single();
            client.Countries = countriesNameValues;
            return client;
        }
    }
}
