using System.ComponentModel.DataAnnotations;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

namespace OrdersDb.Domain.Services.Staff.Position
{
    /// <summary>
    /// Должность
    /// </summary>
    public class Position : EntityBase, INamedEntity
    {
        public override int Id { get; set; }

        /// <summary>
        /// Название должности
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Position_Name", ResourceType = typeof(EntitiesResources))]
        public string Name { get; set; }
    }
}
