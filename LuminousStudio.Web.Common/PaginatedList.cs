namespace LuminousStudio.Web.Common
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(
            IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var list = source.ToList();
            int count = list.Count;
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var items = list
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedList<T>
            {
                Items = items,
                PageIndex = pageIndex,
                TotalPages = totalPages,
                TotalItems = count,
                PageSize = pageSize
            };
        }
    }
}