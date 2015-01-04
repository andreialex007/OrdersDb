using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

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
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Country_Code", ResourceType = typeof(EntitiesResources))]
        public string Code { get; set; }

        /// <summary>
        /// Название страны по английски
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Country_Name", ResourceType = typeof(EntitiesResources))]
        public string Name { get; set; }

        /// <summary>
        /// Название страны по русски
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Country_RussianName", ResourceType = typeof(EntitiesResources))]
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
