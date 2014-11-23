using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.Region
{
    public class RegionDto : NamedDtoBase
    {
        public RegionDto()
        {
            Countries = new List<NameValue>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public List<NameValue> Countries { get; set; } 
    }
}
