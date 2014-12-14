using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Accounts.User
{
    public interface IUserService : INamedServiceBase<User, UserSearchParameters, UserDto>
    {
        User Login(string name, string password);
        UserDto GetByUserName(string userName);
    }
}