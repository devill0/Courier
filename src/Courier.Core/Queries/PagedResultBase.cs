namespace Courier.Core.Queries
{
    public class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int ResultsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }
}
