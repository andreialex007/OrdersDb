using System.Collections.Generic;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Production.Client
{
    public class ClientDto : NamedDtoBase
    {
        public string FullName { get; set; }
        public string INN { get; set; }
        public string OGRN { get; set; }
        public HouseDto Location { get; set; }


        public string StreetName { get; set; }
        public int StreetId { get; set; }
        public IEnumerable<NameValue> Streets { get; set; }

        public string CityName { get; set; }
        public int CityId { get; set; }
        public List<NameValue> Cities { get; set; }

        public string RegionName { get; set; }
        public int RegionId { get; set; }
        public IEnumerable<NameValue> Regions { get; set; }

        public string CountryName { get; set; }
        public int CountryId { get; set; }
        public IEnumerable<NameValue> Countries { get; set; }

        public string FullLocationString
        {
            get
            {
                return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}",
                    CountryName,
                    RegionName,
                    CityName,
                    StreetName,
                    Location != null ? (object)Location.Number : null,
                    Location != null ? Location.Building : string.Empty,
                    Location != null ? Location.PostalCode : string.Empty);
            }
        }
    }
}
