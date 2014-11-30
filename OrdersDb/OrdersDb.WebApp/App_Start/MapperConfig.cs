using System.Linq;
using System.Reflection;

namespace OrdersDb.WebApp
{
    public class MapperConfig
    {
        public static void Config()
        {
            var domainAssembly = Assembly.Load("OrdersDb.Domain");
            var executingAssembly = Assembly.GetExecutingAssembly();

            var viewModelTypes = executingAssembly.GetTypes()
                .Where(x => x.Name.EndsWith("ViewModel"))
                .Select(x => new { Name = x.Name.Replace("ViewModel", string.Empty), Type = x })
                .ToList();

            var viewModelTypeNames = viewModelTypes.Select(x => x.Name);
            var mapping = domainAssembly.GetTypes().Where(x => viewModelTypeNames.Contains(x.Name))
                .Select(x => new { Type = x, ViewModelType = viewModelTypes.First(v => v.Name == x.Name).Type })
                .ToList();
        }
    }

}
