using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services._Common.Entities;

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
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор города в котором находится улица
        /// </summary>
        [Required]
        [Min(1)]
        public int CityId { get; set; }

        /// <summary>
        /// город в котором находится улица
        /// </summary>
        [Required]
        public City.City City { get; set; }

        /// <summary>
        /// Список домов находящихся на улице
        /// </summary>
        public List<House> Houses { get; set; }
    }
}
