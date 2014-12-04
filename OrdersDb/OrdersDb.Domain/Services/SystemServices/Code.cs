using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrdersDb.Domain.Services.Orders.Order;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.SystemServices
{
    /// <summary>
    /// Уникальный код заказа в базе
    /// </summary>
    public class Code : EntityBase
    {
        public override int Id { get; set; }

        /// <summary>
        /// Значение кода
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Заказ привязанный к коду
        /// </summary>
        public Order Order { get; set; }
    }
}
