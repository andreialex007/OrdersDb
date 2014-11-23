using System;
using System.ComponentModel.DataAnnotations;

namespace OrdersDb.Domain.Services._Common.Entities
{
    public abstract class EntityBase : IdEntity
    {
        /// <summary>
        /// Идентификатор объекта в базе данных
        /// </summary>
        [Key]
        public abstract int Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime Modified { get; set; }
    }
}
