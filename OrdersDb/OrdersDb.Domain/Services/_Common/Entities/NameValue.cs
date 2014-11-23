namespace OrdersDb.Domain.Services._Common.Entities
{
    public class NameValue
    {
        public NameValue()
        {

        }

        public NameValue(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
