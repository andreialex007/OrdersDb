namespace OrdersDb.Domain.Services.Production.Client
{
    public static class ClientExtensions
    {
        public static string GetLocationString(this Client client)
        {
            return string.Format("{0} {1} {2} {3} {4} {5} ",
                client.Location.Street.City.Region.Country.Name,
                client.Location.Street.City.Region.Name,
                client.Location.Street.City.Name,
                client.Location.Street.Name,
                client.Location.Building,
                client.Location.Number);
        }
    }
}
