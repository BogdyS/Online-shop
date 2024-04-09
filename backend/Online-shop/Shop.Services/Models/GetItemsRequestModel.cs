using Shop.Services.Common;

namespace Shop.Services.Models
{
    public class GetItemsRequestModel
    {
        public PaginationRequest Pagination { get; set; } = null!;
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? Search { get; set; }
        public List<int> CategoriesIds { get; set; } = new();
    }
}
