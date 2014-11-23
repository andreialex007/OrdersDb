using System.Web.Mvc;
using OrdersDb.Domain.Services.Staff.Employee;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItem(Icon = "fa-user-md", Name = "Сотрудники")]
    public class EmployeesController : ControllerBase<IEmployeeService, Employee, EmloyeeSearchParameters, EmployeeDto>
    {
        public EmployeesController(IEmployeeService service)
            : base(service)
        {
        }
    }
}