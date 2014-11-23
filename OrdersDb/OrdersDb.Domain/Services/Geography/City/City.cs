using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.City
{
    /// <summary>
    /// Город
    /// </summary>
    public class City : EntityBase, INamedEntity
    {
        public City()
        {
            Streets = new List<Street.Street>();
        }

        public override int Id { get; set; }

        /// <summary>
        /// Имя города
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор региона
        /// </summary>
        [Required]
        public int RegionId { get; set; }

        /// <summary>
        /// Регион в котором находится данный город
        /// </summary>
        [Required]
        public Region.Region Region { get; set; }

        /// <summary>
        /// Численность населения
        /// </summary>
        [Min(1000)]
        public int Population { get; set; }

        /// <summary>
        /// Список улиц в данном городе
        /// </summary>
        public List<Street.Street> Streets { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, RegionId: {2}, Region: {3}, Population: {4}", Id, Name, RegionId, Region, Population);
        }
    }
}
