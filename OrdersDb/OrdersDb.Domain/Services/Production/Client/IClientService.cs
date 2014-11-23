using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Production.Client
{
    public interface IClientService : IServiceBase<Client, ClientSearchParameters, ClientDto>
    {
    }
}