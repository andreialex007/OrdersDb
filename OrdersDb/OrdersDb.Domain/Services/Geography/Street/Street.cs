using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

namespace OrdersDb.Domain.Services.Geography.Street
{
    public class Street : EntityBase, INamedEntity
    {
        public Street()
        {
            Houses = new List<House>();
        }

        public override int Id { get; set; }

        /// <summary>
        /// Название улицы
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор города в котором находится улица
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Min(1, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "ValueMustBeSpecified")]
        public int CityId { get; set; }

        /// <summary>
        /// город в котором находится улица
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public City.City City { get; set; }

        /// <summary>
        /// Список домов находящихся на улице
        /// </summary>
        public List<House> Houses { get; set; }
    }
}
