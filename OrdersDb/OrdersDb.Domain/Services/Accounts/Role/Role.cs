﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Accounts.Role
{
    /// <summary>
    /// Роль пользователя в системе
    /// </summary>
    public class Role : EntityBase, INamedEntity
    {
        
        public Role()
        {
            Users = new List<User.User>();
            Permissions = new PermissionsItem();
        }

        public override int Id { get; set; }
        
        /// <summary>
        /// Название роли
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Список пользоателей имеющих данную роль
        /// </summary>
        public List<User.User> Users { get; set; }

        /// <summary>
        /// Разрешения данной роли
        /// </summary>
        public PermissionsItem Permissions { get; set; }
    }
}
