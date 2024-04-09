using Microsoft.Extensions.Options;
using Online_Shop.UI.Models;
using Online_Shop.UI.Options;

namespace Online_Shop.UI.Pages
{
    public class AddItemModel : BasePage
    {
        public AddItemModel(IOptions<ApiGatewayOptions> options) : base(options) { }
        
        public void OnGet()
        {
        }
    }
}
