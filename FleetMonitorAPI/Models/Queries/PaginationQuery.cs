namespace FleetMonitorAPI.Models.Queries
{
    public class PaginationQuery
    {
        public string? Phrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
