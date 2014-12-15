using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Exceptions;
using OrdersDb.Domain.Services.Accounts.Role;
using OrdersDb.Domain.Services.Geography.Country;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;
using System.Data.Entity;

namespace OrdersDb.Domain.Services.Accounts.User
{
    public class UserService : NamedServiceBase<User, UserSearchParameters, UserDto>, IUserService
    {
        public UserService(IAppDbContext db, IObjectContext context)
            : base(db, context)
        {
        }

        public override void Add(User entity)
        {
            entity.Image = File.ReadAllBytes(HttpContext.Session.GetImagePath<User>(x => x.Image));
            entity.GetValidationErrors().ThrowIfHasErrors();
            Db.AttachIfDetached(entity);
            entity.Roles.ForEach(x => Db.Roles.Attach(x));
            Db.Entry(entity).State = EntityState.Added;
            Db.SaveChanges();
            HttpContext.Session.ClearImagePath<User>(x => x.Image);
        }

        public override void Update(User entity)
        {
            var errors = entity.GetValidationErrors(x => x.Name, x => x.Email).ToList();
            if (!string.IsNullOrEmpty(entity.Password))
                errors.AddRange(entity.GetValidationErrors(x => x.Password));
            errors.ThrowIfHasErrors();

            ProcessEntity(entity);
            Db.SaveChanges();
            HttpContext.Session.ClearImagePath<User>(x => x.Image);
        }

        public byte[] GetUserImage(string userName)
        {
            var user = Db.Set<User>().Single(x => x.Name.ToLower() == userName.ToLower());
            return user.Image;
        }

        private void ProcessEntity(User entity)
        {
            entity.Roles.ForEach(x => Db.Roles.Attach(x));
            var dbUser = Db.Users.Include(x => x.Roles).Single(x => x.Id == entity.Id);
            dbUser.Roles.Clear();
            dbUser.Roles.AddRange(entity.Roles);
            dbUser.Name = entity.Name;
            dbUser.Email = entity.Email;
            if (!string.IsNullOrEmpty(entity.Password))
                dbUser.Password = PasswordHasher.HashPassword(entity.Password);
        }

        public override List<UserDto> Search(UserSearchParameters @params)
        {
            var query = Db.Set<User>()
                .Include(x => x.Roles)
                .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (!string.IsNullOrEmpty(@params.Email))
                query = query.Where(x => x.Email.ToLower().Contains(@params.Email.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new UserDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  Email = x.Email
                                                              }).ToList();
        }

        public User Login(string name, string password)
        {
            ValidateNameAndPassword(name, password);
            var hash = PasswordHasher.HashPassword(password);
            var user = Db.Users
                .Include(x => x.Roles)
                .SingleOrDefault(x => x.Name.ToLower() == name.ToLower() && x.Password.ToLower() == hash.ToLower());

            if (user == null)
                throw new ValidationException("Login or password incorrect.");
            return user;
        }

        private void ValidateNameAndPassword(string name, string password)
        {
            var user = new User { Name = name, Password = password };
            var errors = user.GetValidationErrors(x => x.Name, x => x.Password);
            errors.ThrowIfHasErrors();
        }

        public UserDto GetByUserName(string userName)
        {
            var user = Db.Users.SingleOrDefault(x => x.Name.ToLower() == userName.ToLower());
            return new UserDto
                   {
                       Id = user.Id,
                       Name = user.Name,
                       Email = user.Email
                   };
        }

        public override UserDto GetById(int id)
        {
            var query = Db.Users.Include(x => x.Roles).AsQueryable();
            var allRoles = Db.Roles
                .AsQueryable()
                .OrderBy(x => x.Name)
                .Select(x => new RoleDto { Id = x.Id, Name = x.Name })
                .ToList();

            var userDto = new UserDto();

            if (id != 0)
                userDto = query.Where(x => x.Id == id)
                    .Select(x => new UserDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Email = x.Email,
                                     Roles = x.Roles.Select(r => new RoleDto { Id = r.Id, Name = r.Name }).ToList()
                                 }).Single();

            allRoles.ForEach(x =>
                             {
                                 x.IsSelected = userDto.Roles.Any(r => r.Id == x.Id);
                             });
            userDto.Roles = allRoles;
            return userDto;
        }
    }
}
