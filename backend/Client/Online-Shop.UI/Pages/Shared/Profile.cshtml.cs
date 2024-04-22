using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Online_Shop.UI.Models;
using Online_Shop.UI.Options;

namespace Online_Shop.UI.Pages.Shared
{
    public class ProfileModel : BasePage
    {
        public ProfileModel(IOptions<ApiGatewayOptions> options) : base(options)
        {
        }

        public void OnGet()
        {
        }
    }
}
