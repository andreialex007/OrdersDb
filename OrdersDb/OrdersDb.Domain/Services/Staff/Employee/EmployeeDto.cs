using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Staff.Employee
{
    public class EmployeeDto : DtoBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string SNILS { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<NameValue> AvaliablePositions { get; set; }

        //location
        public HouseDto Location;
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
    }
}
