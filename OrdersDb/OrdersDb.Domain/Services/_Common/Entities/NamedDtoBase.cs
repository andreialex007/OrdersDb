using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDb.Domain.Services._Common.Entities
{
    public class NamedDtoBase : DtoBase
    {
        public string Name { get; set; }
    }
}
