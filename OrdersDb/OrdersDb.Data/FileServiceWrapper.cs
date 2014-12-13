using System;
using System.IO;
using System.Web.Hosting;
using OrdersDb.Domain.Services.SystemServices;

namespace OrdersDb.Data
{
    public class FileServiceWrapper : IFileService
    {
        public string GetTemporaryFolder()
        {
            return HostingEnvironment.MapPath("~/Files/");
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            System.IO.File.WriteAllBytes(path, bytes);
        }


        public byte[] ReadAllBytes(string path)
        {
            if (!File.Exists(path))
                return null;
            return System.IO.File.ReadAllBytes(path);
        }

        public bool FileExists(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}
