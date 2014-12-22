using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using OrdersDb.Domain.Services.Accounts.Role;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Code.Extensions;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-puzzle-piece", ResourceType = typeof(EntitiesResources), ResourcePropertyName = "Roles")]
    public class RolesController : NamedEntityControllerBase<IRoleService, Role, RoleSearchParameters, RoleDto>
    {
        public RolesController(IRoleService service)
            : base(service)
        {
        }

        public override ActionResult GetById(int id)
        {
            var roleDto = _service.GetById(id);
            var permissions = this.GetAllAvaliablePermissions();
            permissions.ForEach(x =>
                                {
                                    if (x.AccessType == AccessType.Read)
                                        x.Checked = roleDto.Permissions.Reads.Any(p => p == x.Code);
                                    if (x.AccessType == AccessType.Delete)
                                        x.Checked = roleDto.Permissions.Deletes.Any(p => p == x.Code);
                                    if (x.AccessType == AccessType.Add)
                                        x.Checked = roleDto.Permissions.Adds.Any(p => p == x.Code);
                                    if (x.AccessType == AccessType.Update)
                                        x.Checked = roleDto.Permissions.Updates.Any(p => p == x.Code);
                                });

            if (roleDto.Name == RoleService.AdminRoleName)
            {
                permissions.ForEach(x => x.Checked = true);
            }

            var permissionGroups = permissions.GroupBy(x => x.Name).Select(x => new { Name = x.Key, Permissions = x.ToList() }).ToList();

            var jsonResult = Json(new
                                  {
                                      IsReadOnly = roleDto.Name == RoleService.AdminRoleName,
                                      roleDto,
                                      permissionGroups
                                  });
            return jsonResult;
        }

        [NonAction]
        public override ActionResult Add(Role region)
        {
            return base.Add(region);
        }

        [NonAction]
        public override ActionResult Update(Role region)
        {
            return base.Update(region);
        }

        [HttpPost]
        public ActionResult Add(Role role, List<PermissionViewModel> permissions)
        {
            role.Permissions = permissions.ToPermissionsItem();
            _service.Add(role);
            return SuccessJsonResult();
        }

        [HttpPost]
        public ActionResult Update(Role role, List<PermissionViewModel> permissions)
        {
            role.Permissions = permissions.ToPermissionsItem();
            _service.Update(role);
            return SuccessJsonResult();
        }
    }

}