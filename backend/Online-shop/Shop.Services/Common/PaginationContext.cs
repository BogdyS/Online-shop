namespace Shop.Services.Common
{
    public class PaginationContext
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
