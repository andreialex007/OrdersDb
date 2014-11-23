using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Accounts.Role
{
    public interface IRoleService : INamedServiceBase<Role, RoleSearchParameters, RoleDto>
    {
        PermissionsItem GetPermissionsForRole(string roleName);
    }
}