using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services.Production.Client;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

namespace OrdersDb.Domain.Services.Geography.Hose
{
    /// <summary>
    /// Дом
    /// </summary>
    public class House : EntityBase
    {
        public House()
        {
            Clients = new List<Client>();
        }

        public override int Id { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        [Min(1, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "ValueMustBeSpecified")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public int Number { get; set; }

        /// <summary>
        /// Корпус
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Индекс
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Идентификатор улицы
        /// </summary>
        [Min(1, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "ValueMustBeSpecified")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public int StreetId { get; set; }

        /// <summary>
        /// Улица на которой находится дом
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public Street.Street Street { get; set; }

        /// <summary>
        /// Клиенты находящиеся в данном доме
        /// </summary>
        public List<Client> Clients { get; set; }
    }
}
