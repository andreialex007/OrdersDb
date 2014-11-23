using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using OrdersDb.Domain.Services.Accounts.Role;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Code.Extensions
{
    public static class Utils
    {
        public static string MapPathReverse(string fullServerPath)
        {
            return string.Format(@"~\{0}", fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty));
        }

        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            var target = new MemoryStream();
            file.InputStream.CopyTo(target);
            return target.ToArray();
        }

        public static bool IsJsonRequest(this HttpContext contextBase)
        {
            return contextBase.Request.ContentType.IndexOf("application/json", StringComparison.OrdinalIgnoreCase) != -1;
        }


        public static PermissionsItem ToPermissionsItem(this List<PermissionViewModel> permissions)
        {
            var list = permissions.Where(x => x.Checked).ToList();
            var reads = list.Where(x => x.AccessType == AccessType.Read).Select(x => x.Code).ToList();
            var adds = list.Where(x => x.AccessType == AccessType.Add).Select(x => x.Code).ToList();
            var updates = list.Where(x => x.AccessType == AccessType.Update).Select(x => x.Code).ToList();
            var deletes = list.Where(x => x.AccessType == AccessType.Delete).Select(x => x.Code).ToList();

            return new PermissionsItem
             {
                 Adds = adds,
                 Reads = reads,
                 Updates = updates,
                 Deletes = deletes,
             };
        }
    }
}