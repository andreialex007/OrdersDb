using System.Collections.Generic;
using System.Linq;
using OrdersDb.Domain.Services._Common;
using System.Data.Entity;
using OrdersDb.Domain.Services._Common.Entities;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

namespace OrdersDb.Domain.Services.Staff.Employee
{
    public class EmployeeService : ServiceBase<Employee, EmloyeeSearchParameters, EmployeeDto>, IEmployeeService
    {
        public EmployeeService(IAppDbContext db, IObjectContext objectContext)
            : base(db, objectContext)
        {
        }

        public override List<EmployeeDto> Search(EmloyeeSearchParameters @params)
        {
            var query = Db.Employees
                .Include(x => x.Position)
                .AsQueryable();

            query = SearchByIds(query, @params);

            if (!string.IsNullOrEmpty(@params.FirstName))
                query = query.Where(x => x.FirstName.ToLower().Contains(@params.FirstName.ToLower()));

            if (!string.IsNullOrEmpty(@params.LastName))
                query = query.Where(x => x.LastName.ToLower().Contains(@params.LastName.ToLower()));

            if (!string.IsNullOrEmpty(@params.Patronymic))
                query = query.Where(x => x.Patronymic.ToLower().Contains(@params.Patronymic.ToLower()));

            if (!string.IsNullOrEmpty(@params.Email))
                query = query.Where(x => x.Email.ToLower().Contains(@params.Email.ToLower()));

            if (!string.IsNullOrEmpty(@params.SNILS))
                query = query.Where(x => x.SNILS.ToLower().Contains(@params.SNILS.ToLower()));

            if (!string.IsNullOrEmpty(@params.PositionName))
                query = query.Where(x => x.Position.Name.ToLower().Contains(@params.PositionName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new EmployeeDto
                                                              {
                                                                  Id = x.Id,
                                                                  FirstName = x.FirstName,
                                                                  LastName = x.LastName,
                                                                  Patronymic = x.Patronymic,
                                                                  Email = x.Email,
                                                                  PositionName = x.Position != null ? x.Position.Name : string.Empty,
                                                                  SNILS = x.SNILS
                                                              }).ToList();
        }

        public override EmployeeDto GetById(int id)
        {
            var employeeDto = new EmployeeDto();

            if (id != 0)
            {
                employeeDto = Db.Employees
                    .Include(x => x.Residence.Street.City.Region.Country)
                    .Include(x => x.Position)
                    .AsQueryable()
                    .Where(x => x.Id == id)
                    .Select(x => new EmployeeDto
                                 {
                                     Id = x.Id,
                                     Email = x.Email,
                                     FirstName = x.FirstName,
                                     LastName = x.LastName,
                                     PositionId = x.PositionId,
                                     PositionName = x.Position.Name,
                                     Patronymic = x.Patronymic,
                                     SNILS = x.SNILS
                                 }).Single();
            }

            employeeDto.AvaliablePositions = Db
                .Set<Position.Position>()
                .Select(x => new NameValue { Id = x.Id, Name = x.Name })
                .ToList();

            return employeeDto;

        }
    }
}
