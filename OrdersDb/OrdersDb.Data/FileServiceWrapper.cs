using System;
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
            return System.IO.File.ReadAllBytes(path);
        }
    }
}
