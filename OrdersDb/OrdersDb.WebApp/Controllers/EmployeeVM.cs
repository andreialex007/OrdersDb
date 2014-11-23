using System.Collections.Generic;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.WebApp.Controllers
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Идентификатор должности пользователя
        /// </summary>
        public int PositionId { get; set; }
        
        /// <summary>
        /// Название выбранной должности
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Cписок возможных должностей
        /// </summary>
        public List<NameValue> AvaliablePositions { get; set; }

        /// <summary>
        /// СНИЛС пользователя
        /// </summary>
        public string SNILS { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Место проживания
        /// </summary>
        public House Residence { get; set; }
    }
}