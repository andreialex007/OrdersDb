using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

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
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        /// <summary>
        /// Города которы находятся в данном регионе
        /// </summary>
        public List<City.City> Cities { get; set; }

        /// <summary>
        /// Идентификатор страны
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Min(1, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "ValueMustBeSpecified")]
        [Display(Name = "Region_Country", ResourceType = typeof(EntitiesResources))]
        public int CountryId { get; set; }

        /// <summary>
        /// Страна в которой находится регион
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Region_Country", ResourceType = typeof(EntitiesResources))]
        public Country.Country Country { get; set; }
    }
}
