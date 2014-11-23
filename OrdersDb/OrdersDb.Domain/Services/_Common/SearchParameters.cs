namespace OrdersDb.Domain.Services._Common
{
    public class SearchParameters
    {

        public SearchParameters()
        {
            OrderBy = null;
            IsAsc = true;
            Take = Skip = null;
        }

        public int[] Ids { get; set; }
        public string OrderBy { get; set; }
        public bool IsAsc { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}