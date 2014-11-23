using System.Collections.Generic;
using OrdersDb.Domain.Services.Accounts.Role;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Accounts.User
{
    public class UserDto : NamedDtoBase
    {
        public UserDto()
        {
            Roles = new List<RoleDto>();
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}