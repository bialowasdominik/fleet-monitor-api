namespace FleetMonitorAPI.Models
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalItem { get; set; }

        public PageResult(List<T> items, int totalItem, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItem = totalItem;
            ItemFrom = pageSize * (pageNumber - 1) + 1;
            ItemTo = ItemFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalItem /(double) pageSize);
        }
    }
}
