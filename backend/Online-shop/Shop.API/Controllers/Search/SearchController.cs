using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.API.Controllers.Search.Operations;

namespace Shop.API.Controllers.Search
{
    public class SearchController : BaseController
    {
        public SearchController(
            IMediator mediator,
            IMapper mapper)
            : base(mediator, mapper)
        {
        }

        [HttpGet("api/search/items/auto-complete")]
        public async Task<IActionResult> GetSearchAutoCompleteAsync(
            [FromQuery] GetSearchAutoComplete.GetSearchAutoCompleteRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();
        }
    }
}
