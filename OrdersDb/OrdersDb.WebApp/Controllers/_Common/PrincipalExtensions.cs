using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using OrdersDb.Domain.Services.Accounts.Role;

namespace OrdersDb.WebApp.Controllers._Common
{
    public static class PrincipalExtensions
    {
        public static List<string> GetRoles(this IPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            var claims = identity.Claims;
            return claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
        }

        public static PermissionsItem GetPermissions(this IPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            var claims = identity.Claims.ToList();
            var addPermissions = claims.Where(x => x.Type == CustomClaimTypes.AddPermissions).Select(x => x.Value).ToList();
            var deletePermissions = claims.Where(x => x.Type == CustomClaimTypes.DeletePermissions).Select(x => x.Value).ToList();
            var readPermissions = claims.Where(x => x.Type == CustomClaimTypes.ReadPermissions).Select(x => x.Value).ToList();
            var updatePermissions = claims.Where(x => x.Type == CustomClaimTypes.UpdatePermissions).Select(x => x.Value).ToList();
            return new PermissionsItem
                   {
                       Adds = addPermissions,
                       Deletes = deletePermissions,
                       Reads = readPermissions,
                       Updates = updatePermissions,
                   };
        }

    }
}