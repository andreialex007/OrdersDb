using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.Country
{
    /// <summary>
    /// Страна
    /// </summary>
    public class Country : EntityBase, INamedEntity
    {
        public Country()
        {
            Regions = new List<Region.Region>();
        }

        /// <summary>
        /// Идентификатор страны
        /// </summary>
        public override int Id { get; set; }

        /// <summary>
        /// Код страны
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Название страны по английски
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Название страны по русски
        /// </summary>
        [Required]
        public string RussianName { get; set; }

        /// <summary>
        /// Картинка с флагом
        /// </summary>
        public byte[] Flag { get; set; }

        /// <summary>
        /// Список регионов
        /// </summary>
        public List<Region.Region> Regions { get; set; }
    }
}
