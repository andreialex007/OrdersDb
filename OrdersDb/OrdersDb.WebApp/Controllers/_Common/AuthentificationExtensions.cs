using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using OrdersDb.Domain.Services.Accounts.Role;
using OrdersDb.Domain.Services.Accounts.User;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.WebApp.Code;

namespace OrdersDb.WebApp.Controllers._Common
{
    public static class AuthentificationExtensions
    {
        public const string @namespace = "OrdersDb.WebApp.Controllers";
        public const string StringValueType = "http://www.w3.org/2001/XMLSchema#string";

        public static void SignIn(this ControllerBase controllerBase, User user, bool isPersistent = false)
        {
            controllerBase.SignIn(user.Name, isPersistent, user.Roles.ToArray());
        }

        public static void SetControlsConfiguration(this ControllerBase controllerBase, PermissionsItem permissions)
        {
            var cookie = new HttpCookie("Permissions") { Value = JsonConvert.SerializeObject(permissions), Path = "/", Expires = DateTime.MaxValue };
            controllerBase.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
        }

        public static void SignIn(this ControllerBase controllerBase, string userName, bool isPersistent = false, params Role[] roles)
        {
            var authenticationManager = controllerBase.HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignIn();
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Name, userName, StringValueType));

            //Добавляем роли в клеймы
            foreach (var role in roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role.Name, StringValueType));

            var permissions = GetPermissionsWhichExistsInAllRoles(roles);
            AddPermissionsToClaims(identity, permissions);

            controllerBase.SetControlsConfiguration(permissions);
            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        private static PermissionsItem GetPermissionsWhichExistsInAllRoles(params Role[] roles)
        {
            var adminroleName = roles.SingleOrDefault(x => x.Name == RoleService.AdminRoleName);
            if (adminroleName != null)
            {
                adminroleName.Permissions.Read =
                    adminroleName.Permissions.Update =
                        adminroleName.Permissions.Delete =
                            adminroleName.Permissions.Add = string.Join("|", GetAllControllerNames(null));
            }

            var readPermissions = new List<string>();
            foreach (var role in roles)
                readPermissions.AddRange(role.Permissions.Reads.Where(x => roles.All(r => r.Permissions.Reads.Contains(x))));

            var addPermissions = new List<string>();
            foreach (var role in roles)
                addPermissions.AddRange(role.Permissions.Adds.Where(x => roles.All(r => r.Permissions.Adds.Contains(x))));

            var updatePermissions = new List<string>();
            foreach (var role in roles)
                updatePermissions.AddRange(role.Permissions.Updates.Where(x => roles.All(r => r.Permissions.Updates.Contains(x))));

            var deletePermissions = new List<string>();
            foreach (var role in roles)
                deletePermissions.AddRange(role.Permissions.Deletes.Where(x => roles.All(r => r.Permissions.Deletes.Contains(x))));

            return new PermissionsItem
                   {
                       Adds = addPermissions,
                       Reads = readPermissions,
                       Updates = updatePermissions,
                       Deletes = deletePermissions,
                   };
        }

        public static List<string> GetAllControllerNames(this ControllerBase controllerBase)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.Namespace == @namespace)
                .Select(x => x.Name.Replace("Controller", string.Empty).ToLower())
                .ToList();
        }

        public static Dictionary<string, string> GetAllControllerNamesAndCodes(this ControllerBase controllerBase)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.Namespace == @namespace)
                .Where(x => x.GetCustomAttributes<MenuItemAttribute>().Any())
                .Select(x => new
                             {
                                 Key = x.Name.Replace("Controller", string.Empty).ToLower(),
                                 Name = x.GetCustomAttributes<MenuItemAttribute>().First().Name
                             }).ToDictionary(x => x.Key, x => x.Name);
        }

        public static List<PermissionViewModel> GetAllAvaliablePermissions(this ControllerBase controllerBase)
        {
            var allControllerNames = controllerBase.GetAllControllerNamesAndCodes();
            var models = allControllerNames.SelectMany(x => new[]
                                                            {
                                                                new PermissionViewModel
                                                                {
                                                                    Name = x.Value,
                                                                    Code = x.Key,
                                                                    AccessType = AccessType.Read,
                                                                    AccessTypeName = AccessType.Read.GetDescription(),
                                                                    Checked = false
                                                                },
                                                                new PermissionViewModel
                                                                {
                                                                    Name = x.Value,
                                                                    Code = x.Key,
                                                                    AccessType = AccessType.Update,
                                                                    AccessTypeName = AccessType.Update.GetDescription(),
                                                                    Checked = false
                                                                },
                                                                new PermissionViewModel
                                                                {
                                                                    Name = x.Value,
                                                                    Code = x.Key,
                                                                    AccessType = AccessType.Add,
                                                                    AccessTypeName = AccessType.Add.GetDescription(),
                                                                    Checked = false
                                                                },
                                                                new PermissionViewModel
                                                                {
                                                                    Name = x.Value,
                                                                    Code = x.Key,
                                                                    AccessType = AccessType.Delete,
                                                                    AccessTypeName = AccessType.Delete.GetDescription(),
                                                                    Checked = false
                                                                }
                                                            }).ToList();
            return models;
        }

        private static void AddPermissionsToClaims(ClaimsIdentity identity, PermissionsItem permissions)
        {
            foreach (var perm in permissions.Reads)
                identity.AddClaim(new Claim(CustomClaimTypes.ReadPermissions, perm, StringValueType));

            foreach (var perm in permissions.Updates)
                identity.AddClaim(new Claim(CustomClaimTypes.UpdatePermissions, perm, StringValueType));

            foreach (var perm in permissions.Deletes)
                identity.AddClaim(new Claim(CustomClaimTypes.DeletePermissions, perm, StringValueType));

            foreach (var perm in permissions.Adds)
                identity.AddClaim(new Claim(CustomClaimTypes.AddPermissions, perm, StringValueType));
        }

        public static void SignOut(this ControllerBase controllerBase)
        {
            var authenticationManager = controllerBase.HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
        }
    }
}