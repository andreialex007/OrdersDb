using System.Collections.Generic;
using OrdersDb.Domain.Services.Accounts.User;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Accounts.Role
{
    public class RoleDto : NamedDtoBase
    {
        public RoleDto()
        {
            Users = new List<UserDto>();
            Permissions = new PermissionsItem();
        }

        public List<UserDto> Users { get; set; }  
        public PermissionsItem Permissions { get; set; }
        public bool IsSelected { get; set; } 
    }
}
