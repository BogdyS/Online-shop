namespace Shop.Services.Common
{
    public class PaginatedResponse<T>
    {
        public PaginationContext PaginationContext { get; set; }
        public List<T> Result { get; set; }
    }
}
