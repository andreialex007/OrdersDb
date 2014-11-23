using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Wrappers;
using OrdersDb.Domain.Utils;
using System.Data.Entity;

namespace OrdersDb.Domain.Services.Accounts.Role
{
    public class RoleService : NamedServiceBase<Role, RoleSearchParameters, RoleDto>, IRoleService
    {
        public const string AdminUserName = "Administrator";
        public const string AdminRoleName = "Administrators";
        public const string FullPermmissions = "FullPermmissions";

        public RoleService(IAppDbContext db, IObjectContext objectContext)
            : base(db, objectContext)
        {
        }

        public PermissionsItem GetPermissionsForRole(string roleName)
        {
            if (roleName == AdminRoleName)
                return PermissionsItem.FullPermissionsItem;

            return Db.Roles.SingleOrDefault(x => x.Name.ToLower() == roleName).Permissions;
        }

        public override List<RoleDto> Search(RoleSearchParameters @params)
        {
            return Db.Roles.IncludeAll().Select(x => new RoleDto
                                                     {
                                                         Id = x.Id,
                                                         Name = x.Name,
                                                         Permissions = x.Permissions
                                                     }).ToList();
        }

        public override RoleDto GetById(int id)
        {
            var roleDto = new RoleDto();

            if (id != 0)
                roleDto = Db.Roles
                   .Include(x => x.Users)
                   .Where(x => x.Id == id)
                   .Select(x => new RoleDto
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Permissions = x.Permissions
                                }).Single();

            return roleDto;
        }
    }
}
