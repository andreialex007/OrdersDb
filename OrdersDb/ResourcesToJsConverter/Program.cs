using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;

namespace ResourcesToJsConverter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Thread.Sleep(10000);

            if (args.Length == 2)
            {
                var inputFolder = args.First();
                var outputFolder = args.Last();
                var files = Directory.GetFiles(inputFolder, "*.resx");
                foreach (var file in files)
                {
                    ConvertResxFileToJsFile(file, outputFolder);
                }
            }
        }

        private static void ConvertResxFileToJsFile(string resxFile, string outputFolder)
        {
            var fileName = Path.GetFileNameWithoutExtension(resxFile);
            var rsxr = new ResXResourceReader(resxFile);
            var stringBuilder = new StringBuilder();
            foreach (DictionaryEntry d in rsxr)
            {
                stringBuilder.AppendFormat("'{0}' : '{1}',{2}", d.Key, d.Value, Environment.NewLine);
            }

            var resultString = stringBuilder.ToString().TrimEnd(string.Format(",{0}", Environment.NewLine).ToCharArray());
            var variableName = fileName.Split(".".ToCharArray()).First();
            var fileContent = " " + variableName + " = { \r\n" + resultString + Environment.NewLine + " }";

            var langFolderName = string.Empty;
            if (fileName.Contains("."))
            {
                langFolderName = fileName.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Last();
                fileName = fileName.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).First();
            }
            else
            {
                langFolderName = "en";
            }
            var langFolderPath = Path.Combine(outputFolder, langFolderName);
            Directory.CreateDirectory(langFolderPath);
            File.WriteAllText(Path.Combine(langFolderPath, string.Format("{0}.js", fileName)), fileContent,Encoding.UTF8);
        }
    }
}
