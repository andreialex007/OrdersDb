using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Microsoft.CSharp;

namespace ViewModelGenerator
{
    static class Program
    {

        private static CodeNamespace _codeNamespace;
        private static CodeTypeDeclaration _codeTypeDeclaration;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var classes = Directory.GetFiles(@"D:\Development\OrdersDb\OrdersDb\OrdersDb.Domain\Entities\").ToList().ConvertAll(x => new
                                                                                                                                            {
                                                                                                                                                path = x,
                                                                                                                                                name = Path.GetFileNameWithoutExtension(x)
                                                                                                                                            });

            var properties = GetPropertiesFromFile(@"D:\Development\OrdersDb\OrdersDb\OrdersDb.Domain\Entities\City.cs");
            var dictionary = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                var name = property.Name;


                var returnType = property.ReturnType.ToString();
                var found = classes.FirstOrDefault(x => x.name == returnType);
                if (found != null)
                {
                    if (GetPropertiesFromFile(found.path).Any(x => x.Name == "Name"))
                    {
                      //  dictionary.Add(string.Format("{0}Name", name), "string");
                    }
                }
                else
                {
                    dictionary.Add(name, returnType);
                }
            }

            //            syntaxTree.


            Init();
            CreateClass("MyViewModel");

            foreach (var item in dictionary)
            {
                CreateProperty(item.Key, item.Value);
            }

            Generate(@"C:\");
        }

        #region Analyze

        public static List<EntityDeclaration> GetPropertiesFromFile(string path)
        {
            var syntaxTree = new CSharpParser().Parse(File.ReadAllText(path));
            return ((syntaxTree.Members.Last() as NamespaceDeclaration).Members.Last() as TypeDeclaration).Members.ToList();
        }

        #endregion


        #region CodeDom

        public static void Init()
        {
            _codeNamespace = new CodeNamespace();
            _codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
        }

        public static void CreateClass(string name)
        {
            _codeTypeDeclaration = new CodeTypeDeclaration("MyViewModel");
            _codeNamespace.Types.Add(_codeTypeDeclaration);
        }

        public static void CreateProperty(string name, string type)
        {
            var snippet = new CodeSnippetTypeMember { Text = string.Format("public {0} {1} {{ get; set; }}", type, name) };
            _codeTypeDeclaration.Members.Add(snippet);
        }

        public static void Generate(string outputFolder)
        {
            using (var codeFile = File.Open(Path.Combine(outputFolder, string.Format("{0}.cs", _codeTypeDeclaration.Name)), FileMode.Create))
            using (var streamWriter = new StreamWriter(codeFile))
            {
                var cSharpCodeProvider = new CSharpCodeProvider();
                var codeGenerator = cSharpCodeProvider.CreateGenerator(streamWriter);
                var codeGeneratorOptions = new CodeGeneratorOptions();
                codeGenerator.GenerateCodeFromNamespace(_codeNamespace, streamWriter, codeGeneratorOptions);
            }
        }

        #endregion
    }
}
