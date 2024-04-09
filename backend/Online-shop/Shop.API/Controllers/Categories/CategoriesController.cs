using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.API.Common.Responses;
using Shop.API.Controllers.Categories.DTO;
using Shop.API.Controllers.Categories.Operations;
using Shop.API.Controllers.Items.DTO.InputDto;

namespace Shop.API.Controllers.Categories
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(
            IMediator mediator,
            IMapper mapper)
            : base(mediator, mapper)
        {
        }

        [HttpGet(Routes.Items.Categories.GetCategories)]
        [ProducesResponseType(type: typeof(ICollection<CategoriesResponseDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var request = new GetCategories.GetCategoriesRequest();

            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();
        }
    }
}
