using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using FizzWare.NBuilder;
using NLipsum.Core;
using OrdersDb.Data.Tools;
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
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;

namespace OrdersDb.Data
{
    public class AppDbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var lipsumGenerator = new LipsumGenerator();

            #region countries

            var russia = new Country { Code = "RUS", Name = "Russia", RussianName = "Россия" };
            context.Countries.AddRangeWithDates(new[] { russia });
            context.SaveChanges();

            #endregion

            #region regions

            var regions = ConvertTools.TableTo(FileFromInitialDataDir("regions.html"), x => new Region
                                                                                            {
                                                                                                Name = x[0],
                                                                                                CountryId = russia.Id
                                                                                            });
            context.Regions.AddRangeWithDates(regions);
            context.SaveChanges();

            #endregion

            #region cities

            var cities = ConvertTools.TableTo(FileFromInitialDataDir("cities.html"), x =>
                                                                                     {
                                                                                         var s = x[4].Replace(",", string.Empty);
                                                                                         var population = int.Parse(s);
                                                                                         return new City
                                                                                                {
                                                                                                    Name = x[2],
                                                                                                    Population = population,
                                                                                                    RegionId = regions.Random().Id
                                                                                                };
                                                                                     });
            context.Cities.AddRangeWithDates(cities);
            context.SaveChanges();

            #endregion

            #region streets

            var streets = ConvertTools.TableTo(FileFromInitialDataDir("streets.html"), x => new Street
                                                                                            {
                                                                                                Name = x[0],
                                                                                                CityId = cities.Random().Id
                                                                                            }).Where(x => !string.IsNullOrEmpty(x.Name))
                                                                                            .Take(500)
                                                                                            .ToList();
            context.Streets.AddRangeWithDates(streets);
            context.SaveChanges();

            #endregion

            #region houses

            var generator = new RandomGenerator();
            var houses = Builder<House>.CreateListOfSize(200).All()
                .With(x => x.Number = generator.Next(0, 1000))
                .With(x => x.Building = generator.Phrase(1))
                .With(x => x.PostalCode = generator.Next(100000, 999999).ToString())
                .With(x => x.StreetId = streets.Random().Id)
                .Build();

            context.Houses.AddRangeWithDates(houses);
            context.SaveChanges();
            #endregion

            #region categories

            var list = ConvertTools.TableToList(FileFromInitialDataDir("categories.html")).Distinct().ToList();
            var firstLevel = list.Take(20);
            var secondLevel = list.Skip(20).Take(60);
            var thirdLevel = list.Skip(80);

            var firstLevelCategories = firstLevel.Select(x => new Category
                                                    {
                                                        Name = x[0],
                                                        Description = lipsumGenerator.GenerateParagraphs(5)[0]
                                                    }).Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            context.Categories.AddRangeWithDates(firstLevelCategories);
            context.SaveChanges();

            var secondLevelCategories = secondLevel.Select(x => new Category
                                                                {
                                                                    Name = x[0],
                                                                    Description = lipsumGenerator.GenerateParagraphs(5)[0],
                                                                    CategoryId = firstLevelCategories.Random().Id
                                                                }).Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            context.Categories.AddRangeWithDates(secondLevelCategories);
            context.SaveChanges();

            var thirdLevelCategories = thirdLevel.Select(x => new Category
                                                                            {
                                                                                Name = x[0],
                                                                                Description = lipsumGenerator.GenerateParagraphs(5)[0],
                                                                                CategoryId = secondLevelCategories.Random().Id
                                                                            }).Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            context.Categories.AddRangeWithDates(thirdLevelCategories);
            context.SaveChanges();

            var categories = firstLevelCategories.Concat(secondLevelCategories).Concat(thirdLevelCategories);


            #endregion

            #region products

            var products = ConvertTools.TableTo(FileFromInitialDataDir("products.html"), x => new Product
                                                                                              {
                                                                                                  Name = x[0],
                                                                                                  BuyPrice = generator.Next(500, 2000),
                                                                                                  SellPrice = generator.Next(500, 2000),
                                                                                                  IsService = generator.Next(0, 100) % 2 == 0,
                                                                                                  CategoryId = categories.Random().Id
                                                                                              }).Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            context.Products.AddRangeWithDates(products);
            context.SaveChanges();

            #endregion

            #region codes

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            Func<string> func = () => new string(
                Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());

            var codesList = Enumerable.Range(0, 1000)
                .Select(x => new Code { Value = func() })
                .ToList();

            context.Codes.AddRangeWithDates(codesList);
            context.SaveChanges();

            #endregion

            #region organizations

            var orgType = new[] { "ООО", "ЗАО", "ОАО" };
            var organizations = ConvertTools.TableTo(FileFromInitialDataDir("organizations.html"), x => new Client
                                                                                                        {
                                                                                                            FullName = string.Format("{0} {1}", orgType.Random(), x[2]),
                                                                                                            Name = x[2],
                                                                                                            INN = generator.Next(1000000000, 9999999999).ToString(),
                                                                                                            OGRN = generator.Next(1000000000000, 9999999999999).ToString(),
                                                                                                            Location = houses.Random()
                                                                                                        }).Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            context.Clients.AddRangeWithDates(organizations);
            context.SaveChanges();

            #endregion

            #region Positions

            var positions = ConvertTools.TableTo(FileFromInitialDataDir("positions.html"), x => new Position
            {
                Name = x[0]
            });
            context.Positions.AddRangeWithDates(positions);
            context.SaveChanges();

            #endregion

            #region employees

            var employees = ConvertTools.TableTo(FileFromInitialDataDir("employees.html"), x => new Employee
                                                                                                {
                                                                                                    LastName = x[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0],
                                                                                                    FirstName = x[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1],
                                                                                                    Patronymic = x[0].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[2],
                                                                                                    Email = string.Format("{0}@yandex.ru", codesList.Random().Value),
                                                                                                    PositionId = positions.Random().Id,
                                                                                                    SNILS = generator.Next(10000000, 99999999).ToString()
                                                                                                }).ToList();
            context.Employees.AddRangeWithDates(employees);
            context.SaveChanges();

            #endregion

            #region roles

            const string moderatorRoleName = "Moderator";
            const string moderatorUserName = "Moderator";

            var permissions = new List<string>
                              {
                                  "account",
                                  "categories",
                                  "cities",
                                  "clients",
                                  "countries",
                                  "employees",
                                  "houses",
                                  "positions",
                                  "products",
                                  "regions",
                                  "orders",
//                                  "roles",
//                                  "users",
                                  "index",
                                  "streets"
                              };

            var permissionsItem = new PermissionsItem
                                  {
                                      Adds = permissions,
                                      Reads = permissions,
                                      Deletes = permissions,
                                      Updates = permissions,
                                  };

            var roles = new List<Role>()
                        {
                            new Role{ Name = RoleService.AdminRoleName },
                            new Role{ Name = moderatorRoleName, Permissions = permissionsItem },
                            new Role{ Name = "Менеджер"},
                            new Role{ Name = "Руководитель"},
                            new Role{ Name = "Редактор"},
                            new Role{ Name = "Оператор"},
                            new Role{ Name = "Сотрудник отдела"},
                            new Role{ Name = "Сотрудник подразделения"},
                            new Role{ Name = "Сотрудник удаленного администрирования"},
                            new Role{ Name = "Рядовой пользователь"}
                        };

            context.Roles.AddRangeWithDates(roles);
            context.SaveChanges();

            #endregion

            #region users

            var users = ConvertTools.TableTo(FileFromInitialDataDir("usernames.html"), x => new User
                                                                                            {
                                                                                                Email = string.Format("{0}@yandex.ru", codesList.Random().Value),
                                                                                                Name = x[0],
                                                                                                Password = Domain.Services.Accounts.User.PasswordHasher.HashPassword(generator.Phrase(7)),
                                                                                                Roles = new List<Role>
                                                                                                        {
                                                                                                            roles.Skip(1).Random()
                                                                                                        }
                                                                                            }).ToList();
            context.Users.AddRangeWithDates(users);
            context.Users.AddRangeWithDates(new List<User>
                                            {
                                                new User
                                                {
                                                    Roles = new List<Role>
                                                            {
                                                                roles.First()
                                                            },
                                                    Name = RoleService.AdminUserName,
                                                    Password = Domain.Services.Accounts.User.PasswordHasher.HashPassword("123456"),
                                                    Email = string.Format("{0}@yandex.ru", codesList.Random().Value)
                                                }
                                            });
            context.Users.AddRangeWithDates(new List<User>
                                            {
                                                new User
                                                {
                                                    Roles = new List<Role>
                                                            {
                                                                roles.Skip(1).First()
                                                            },
                                                    Name = moderatorUserName,
                                                    Password = Domain.Services.Accounts.User.PasswordHasher.HashPassword("123456"),
                                                    Email = string.Format("{0}@yandex.ru", codesList.Random().Value)
                                                }
                                            });
            context.SaveChanges();

            #endregion

            #region orders

            var orders = Builder<Order>.CreateListOfSize(100).All()
                .With(x => x.CodeId = codesList.Random().Id)
                .Build();

            context.Orders.AddRangeWithDates(orders);
            context.SaveChanges();

            #endregion

            #region orderitems

            var orderItems = Builder<OrderItem>.CreateListOfSize(500).All()
                .With(x => x.Order = orders.Random())
                .With(x => x.Product = products.Random())
                .With(x => x.Amount = generator.Next(1, 5))
                .Build();

            context.OrderItems.AddRangeWithDates(orderItems);
            context.SaveChanges();

            #endregion

            base.Seed(context);
        }

        private static string FileFromInitialDataDir(string fileName)
        {
            var path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin", "InitialData", fileName);

            return path;
        }
    }

    public static class DbContextExtensions
    {
        public static void AddRangeWithDates<T>(this DbSet<T> set, IEnumerable<T> entities) where T : EntityBase
        {
            foreach (var entity in entities)
            {
                entity.Created = DateTime.Now;
                entity.Modified = DateTime.Now;
            }
            set.AddRange(entities);
        }
    }
}