using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;

namespace OrdersDb.Domain.Services.Staff.Employee
{
    public class Employee : EntityBase
    {
        public override int Id { get; set; }

        /// <summary>
        /// Имя 
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Идентификатор должности пользователя
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Должность пользователя
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public Position.Position Position { get; set; }

        /// <summary>
        /// СНИЛС пользователя
        /// </summary>
        public string SNILS { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        [Email]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ValidationResources), ErrorMessageResourceName = "Required")]
        public string Email { get; set; }

        /// <summary>
        /// Место проживания
        /// </summary>
        public House Residence { get; set; }
    }
}
