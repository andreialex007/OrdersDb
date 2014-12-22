using System.Web.Mvc;
using OrdersDb.Domain.Services.Staff.Employee;
using OrdersDb.Resources;
using OrdersDb.WebApp.Code;
using OrdersDb.WebApp.Controllers._Common;

namespace OrdersDb.WebApp.Controllers
{
    [MenuItemEntityResourceAttribute(Icon = "fa-user-md")]
    public class EmployeesController : ControllerBase<IEmployeeService, Employee, EmloyeeSearchParameters, EmployeeDto>
    {
        public EmployeesController(IEmployeeService service)
            : base(service)
        {
        }
    }
}