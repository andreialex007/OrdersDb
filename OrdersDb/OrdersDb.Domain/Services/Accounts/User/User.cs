using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Accounts.User
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : EntityBase, INamedEntity
    {
        public User()
        {
            Roles = new List<Role.Role>();
        }

        public override int Id { get; set; }

        /// <summary>
        /// Аватар пользователя
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Name { get; set; }

        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Email]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [StringLength(50, MinimumLength = 6)]
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Список ролей принадлежащих пользователю
        /// </summary>
        public List<Role.Role> Roles { get; set; }
    }
}
