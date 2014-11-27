using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Staff.Employee
{
    public class EmloyeeSearchParameters : SearchParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int[] PositionIds { get; set; }
        public string PositionName { get; set; }
        public string SNILS { get; set; }
        public string Email { get; set; }
    }
}