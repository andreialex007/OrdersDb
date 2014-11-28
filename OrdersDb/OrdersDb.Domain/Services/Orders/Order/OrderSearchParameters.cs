using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Orders.Order
{
    public class OrderSearchParameters : SearchParameters
    {
        public string Code { get; set; }
    }
}
