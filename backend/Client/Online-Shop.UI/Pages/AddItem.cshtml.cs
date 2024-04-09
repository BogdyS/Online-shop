using Microsoft.Extensions.Options;
using MyNamespace;
using Online_Shop.UI.Models;
using Online_Shop.UI.Options;

namespace Online_Shop.UI.Pages
{
    public class AddItemModel : BasePage
    {
        private readonly IProductsClient _productsClient;
        public AddItemModel(IOptions<ApiGatewayOptions> options, IProductsClient productsClient) : base(options)
        {
            _productsClient = productsClient;
        }

        public ICollection<CategoriesResponseDto> Categories { get; set; }
        public async Task OnGetAsync()
        {
            Categories = await _productsClient.ListAllAsync();
        }
    }
}
