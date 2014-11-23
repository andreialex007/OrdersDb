using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BuildGenerator
{
    static class Program
    {

        private static string SolutionDirectory
        {
            get
            {
                return new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.Parent.FullName;
            }
        }

        private const string AdminPagesPath = @"\OrdersDb.WebApp\Scripts\models\pages\admin\";
        private const string SavePath = @"\OrdersDb.WebApp\Scripts\models\pages\admin\dirsList.js";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var solutionDir = args.Any() ? args[0] : SolutionDirectory;


            var directories = Directory.GetDirectories(string.Format("{0}{1}", solutionDir, AdminPagesPath))
                .Select(x => new
                             {
                                 new DirectoryInfo(x).Name,
                                 Files = new DirectoryInfo(x)
                                     .GetFiles()
                                     .Select(f => new
                                                  {
                                                      Path = string.Format("Scripts/models/pages/admin/{0}/{1}", new DirectoryInfo(x).Name, Path.GetFileNameWithoutExtension(f.Name)),
                                                      ObjectName = new DirectoryInfo(x).Name + Path.GetFileNameWithoutExtension(f.Name),
                                                      DirectoryName = new DirectoryInfo(x).Name
                                                  })
                                     .ToList()
                             })
                .ToList();
            var filesIncludeList = "";
            var objectNames = "";
            var returnArray = "";
            foreach (var directory in directories)
            {
                var directoryName = directory.Name;
                var directoryFiles = directory.Files;
                filesIncludeList += directoryFiles.Aggregate("", (current, item) => current + "'" + item.Path + "', ");
                var items = directoryFiles.Aggregate("", (current, item) => current + " " + item.ObjectName + ", ");
                objectNames += items;   
                returnArray += directoryFiles.Aggregate("", (current, item) => current + " { func : " + item.ObjectName + ", name: '" + item.ObjectName + "', dir: '" + item.DirectoryName + "' }, ");
            }

            returnArray = returnArray.TrimEnd(", ".ToCharArray());
            filesIncludeList = filesIncludeList.TrimEnd(", ".ToCharArray());
            objectNames = objectNames.TrimEnd(", ".ToCharArray());

            var js = "define([ " + filesIncludeList + " ], function (" + objectNames + ") { return [ " + returnArray + " ]; });";
            var savePath = string.Format("{0}{1}", solutionDir, SavePath);
            File.WriteAllText(savePath, js, Encoding.UTF8);
        }
    }
}
