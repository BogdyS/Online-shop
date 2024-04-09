using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Online_Shop.UI.Options;

namespace Online_Shop.UI.Models
{
    public abstract class BasePage : PageModel
    {
        private readonly ApiGatewayOptions _apiGateway;
        public BasePage(IOptions<ApiGatewayOptions> options)
        {
            _apiGateway = options.Value;
        }

        public ApiGatewayOptions ApiGateway { get { return _apiGateway; } }
    }
}
