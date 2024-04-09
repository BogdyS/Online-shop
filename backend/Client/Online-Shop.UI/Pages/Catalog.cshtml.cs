using Microsoft.Extensions.Options;
using MyNamespace;
using Online_Shop.UI.Models;
using Online_Shop.UI.Options;

namespace Online_Shop.UI.Pages
{
    public class CatalogModel : BasePage
    {
        public IProductsClient ProductsClient { get; }
        public CatalogModel(IOptions<ApiGatewayOptions> options, IProductsClient productsClient) : base(options)
        {
            ProductsClient = productsClient;
        }
    }
}
