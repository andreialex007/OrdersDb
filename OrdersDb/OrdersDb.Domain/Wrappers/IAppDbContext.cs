using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using OrdersDb.Data;
using OrdersDb.Domain.Services.Accounts.Role;
using OrdersDb.Domain.Services.Accounts.User;
using OrdersDb.Domain.Services.Geography.City;
using OrdersDb.Domain.Services.Geography.Country;
using OrdersDb.Domain.Services.Geography.Hose;
using OrdersDb.Domain.Services.Geography.Region;
using OrdersDb.Domain.Services.Geography.Street;
using OrdersDb.Domain.Services.Orders.Order;
using OrdersDb.Domain.Services.Orders.OrderItem;
using OrdersDb.Domain.Services.Production.Category;
using OrdersDb.Domain.Services.Production.Client;
using OrdersDb.Domain.Services.Production.Product;
using OrdersDb.Domain.Services.Staff.Employee;
using OrdersDb.Domain.Services.Staff.Position;
using OrdersDb.Domain.Services.SystemServices;

namespace OrdersDb.Domain.Wrappers
{
    public interface IAppDbContext
    {
        IDbSet<House> Houses { get; set; }
        IDbSet<Street> Streets { get; set; }
        ObjectContext ObjectContext { get; }
        Database Database { get; }
        IDbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        IDbSet<City> Cities { get; set; }
        IDbSet<Region> Regions { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<OrderItem> OrderItems { get; set; }
        IDbSet<Code> Codes { get; set; }
        IDbSet<Client> Clients { get; set; }
        IDbSet<Employee> Employees { get; set; }
        IDbSet<Position> Positions { get; set; }
        IDbSet<Role> Roles { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<Category> Categories { get; set; }
        IDbSet<Country> Countries { get; set; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        DbEntityEntry Entry(object entity);
        void Dispose();
        IDbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}