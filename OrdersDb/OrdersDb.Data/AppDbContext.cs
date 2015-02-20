using System;
using System.Data.Entity;
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

namespace OrdersDb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            //TODO заглушку убрать нужно
            Database.Connection.ConnectionString = @"Data Source=NOTEBOOK-PC;Initial Catalog=OrdersDb;Integrated Security=True";
            Database.SetInitializer(new AppDbInitializer());

            Configuration.ValidateOnSaveEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }


        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOptional(a => a.ParentCategory)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasOptional(u => u.ParentCategory)
                .WithMany(x => x.Categories)
                .HasForeignKey(u => u.CategoryId);


            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {
            //            try
            //            {
            return base.SaveChanges();
            //            }
            //            catch (DataException ex)
            //            {
            //                return 0;
            //            }
        }
    }
}
