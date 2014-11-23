using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Accounts.User
{
    public class UserSearchParameters : NamedSearchParameters
    {
        public string Email { get; set; }
    }
}