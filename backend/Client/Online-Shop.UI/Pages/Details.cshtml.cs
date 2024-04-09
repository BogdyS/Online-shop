using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyNamespace;

namespace Online_Shop.UI.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IProductsClient _productsClient;

        public DetailsModel(IProductsClient productsClient)
        {
            _productsClient = productsClient;
        }

        public DetailedItemDto ItemModel { get; private set; }
        public async Task OnGet([FromRoute] int productId)
        {
            ItemModel = await _productsClient.DetailsAsync(productId);
        }
    }
}
