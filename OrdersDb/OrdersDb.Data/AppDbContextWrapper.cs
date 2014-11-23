using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
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
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Data
{
    public class AppDbContextWrapper : IAppDbContext
    {
        private readonly AppDbContext _appDbContext;

        public ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)_appDbContext).ObjectContext; }
        }

        public Domain.Wrappers.IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return new DbSetWrapper<TEntity>(_appDbContext.Set<TEntity>());
        }

        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _appDbContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return _appDbContext.GetValidationErrors();
        }

        public DbEntityEntry Entry(object entity)
        {
            return _appDbContext.Entry(entity);
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public IDbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return new DbEntityEntryWrapper<TEntity>(_appDbContext.Entry(entity));
        }

        public Database Database
        {
            get { return _appDbContext.Database; }
        }

        public IDbChangeTracker ChangeTracker
        {
            get { return new DbChangeTrackerWrapper(_appDbContext.ChangeTracker); }
        }

        public DbContextConfiguration Configuration
        {
            get { return _appDbContext.Configuration; }
        }

        public Domain.Wrappers.IDbSet<City> Cities
        {
            get { return new DbSetWrapper<City>(_appDbContext.Cities); }
            set { _appDbContext.Cities = ((DbSetWrapper<City>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Region> Regions
        {
            get { return new DbSetWrapper<Region>(_appDbContext.Regions); }
            set { _appDbContext.Regions = ((DbSetWrapper<Region>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<House> Houses
        {
            get { return new DbSetWrapper<House>(_appDbContext.Houses); }
            set { _appDbContext.Houses = ((DbSetWrapper<House>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Street> Streets
        {
            get { return new DbSetWrapper<Street>(_appDbContext.Streets); }
            set { _appDbContext.Streets = ((DbSetWrapper<Street>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Product> Products
        {
            get { return new DbSetWrapper<Product>(_appDbContext.Products); }
            set { _appDbContext.Products = ((DbSetWrapper<Product>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Order> Orders
        {
            get { return new DbSetWrapper<Order>(_appDbContext.Orders); }
            set { _appDbContext.Orders = ((DbSetWrapper<Order>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<OrderItem> OrderItems
        {
            get { return new DbSetWrapper<OrderItem>(_appDbContext.OrderItems); }
            set { _appDbContext.OrderItems = ((DbSetWrapper<OrderItem>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Code> Codes
        {
            get { return new DbSetWrapper<Code>(_appDbContext.Codes); }
            set { _appDbContext.Codes = ((DbSetWrapper<Code>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Client> Clients
        {
            get { return new DbSetWrapper<Client>(_appDbContext.Clients); }
            set { _appDbContext.Clients = ((DbSetWrapper<Client>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Employee> Employees
        {
            get { return new DbSetWrapper<Employee>(_appDbContext.Employees); }
            set { _appDbContext.Employees = ((DbSetWrapper<Employee>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Position> Positions
        {
            get { return new DbSetWrapper<Position>(_appDbContext.Positions); }
            set { _appDbContext.Positions = ((DbSetWrapper<Position>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Role> Roles
        {
            get { return new DbSetWrapper<Role>(_appDbContext.Roles); }
            set { _appDbContext.Roles = ((DbSetWrapper<Role>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<User> Users
        {
            get { return new DbSetWrapper<User>(_appDbContext.Users); }
            set { _appDbContext.Users = ((DbSetWrapper<User>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Category> Categories
        {
            get { return new DbSetWrapper<Category>(_appDbContext.Categories); }
            set { _appDbContext.Categories = ((DbSetWrapper<Category>)value)._dbSet; }
        }

        public Domain.Wrappers.IDbSet<Country> Countries
        {
            get { return new DbSetWrapper<Country>(_appDbContext.Countries); }
            set { _appDbContext.Countries = ((DbSetWrapper<Country>)value)._dbSet; }
        }


        public AppDbContextWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}