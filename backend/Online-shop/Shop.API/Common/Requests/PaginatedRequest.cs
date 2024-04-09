using Microsoft.AspNetCore.Mvc;
using Shop.Services.Common;

namespace Shop.API.Common.Requests
{
    public class PaginatedRequest : BaseRequest
    {
        /// <summary>
        /// Page number (From 1).
        /// </summary>
        [FromQuery]
        public int? Page { get; set; }

        /// <summary>
        /// Page's size.
        /// </summary>
        [FromQuery]
        public int? Size { get; set; }

        public PaginationRequest ToPaginationRequest()
        {
            return new PaginationRequest { Page = Page ?? 1, Size = Size ?? 15 };
        }
    }
}
