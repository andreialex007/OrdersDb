using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Orders.Order
{
    public class OrderDto : DtoBase
    {
        public string Code { get; set; }
    }
}
