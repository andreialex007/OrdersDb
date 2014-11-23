using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services.Orders.Order;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Production.Client
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client : EntityBase, INamedEntity
    {
        public Client()
        {
            Orders = new List<Order>();
        }

        public override int Id { get; set; }

        /// <summary>
        /// Общее имя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        [Required]
        public string INN { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        [Required]
        public string OGRN { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Required]
        public House Location { get; set; }

        /// <summary>
        /// Идентификатор дома
        /// </summary>
        [ForeignKey("Location")]
        public int? LocationId { get; set; }

        /// <summary>
        /// Заказы сделанные данным клиентом
        /// </summary>
        public List<Order> Orders { get; set; }

    }
}
