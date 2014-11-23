using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.Region
{
    /// <summary>
    /// Регион страны
    /// </summary>
    public class Region : EntityBase, INamedEntity
    {
        public Region()
        {
            Cities = new List<City.City>();
        }

        public override int Id { get; set; }

        /// <summary>
        /// Название региона
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Города которы находятся в данном регионе
        /// </summary>
        public List<City.City> Cities { get; set; }

        /// <summary>
        /// Идентификатор страны
        /// </summary>
        [Required]
        public int CountryId { get; set; }

        /// <summary>
        /// Страна в которой находится регион
        /// </summary>
        [Required]
        public Country.Country Country { get; set; }
    }
}
