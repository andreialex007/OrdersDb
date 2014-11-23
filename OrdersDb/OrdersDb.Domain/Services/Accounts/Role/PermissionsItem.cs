using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OrdersDb.Domain.Services.Accounts.Role
{
    public class PermissionsItem
    {
        public PermissionsItem()
        {
            Updates = new List<string>();
            Deletes = new List<string>();
            Adds = new List<string>();
            Reads = new List<string>();
            Update = string.Empty;
            Read = string.Empty;
            Delete = string.Empty;
            Add = string.Empty;
        }

        /// <summary>
        /// Контроллеры на которые измеется разрешение на запись
        /// </summary>
        [JsonIgnore]
        public string Update { get; set; }
        public List<string> Updates
        {
            get { return (Update ?? string.Empty).Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList(); }
            set
            {
                Update = string.Join("|", value);
            }
        }

        /// <summary>
        /// Контроллеры на которые измеется разрешение на чтение
        /// </summary>
        [JsonIgnore]
        public string Read { get; set; }
        public List<string> Reads
        {
            get { return (Read ?? string.Empty).Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList(); }
            set { Read = string.Join("|", value); }
        }

        /// <summary>
        /// Контроллеры на которые измеется разрешение на удаление
        /// </summary>
        [JsonIgnore]
        public string Delete { get; set; }
        public List<string> Deletes
        {
            get { return (Delete ?? string.Empty).Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList(); }
            set { Delete = string.Join("|", value); }
        }

        /// <summary>
        /// Контроллеры на которые измеется разрешение на добавление
        /// </summary>
        [JsonIgnore]
        public string Add { get; set; }
        public List<string> Adds
        {
            get { return (Add ?? string.Empty).Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList(); }
            set
            {
                Add = string.Join("|", value);
            }
        }

        public static PermissionsItem FullPermissionsItem
        {
            get
            {
                return new PermissionsItem
                       {
                           Add = RoleService.FullPermmissions,
                           Delete = RoleService.FullPermmissions,
                           Read = RoleService.FullPermmissions,
                           Update = RoleService.FullPermmissions,
                       };
            }
        }
    }
}
