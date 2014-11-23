using System.ComponentModel.DataAnnotations;
using OrdersDb.Domain.Services._Common.Entities;

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
        [Required]
        public string Name { get; set; }
    }
}
