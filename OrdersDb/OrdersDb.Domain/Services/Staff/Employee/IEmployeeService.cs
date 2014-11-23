using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Staff.Employee
{
    public interface IEmployeeService : IServiceBase<Employee, EmloyeeSearchParameters, EmployeeDto>
    {
    }
}