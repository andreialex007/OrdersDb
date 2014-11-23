using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.Street
{
    public class StreetDto : NamedDtoBase
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
