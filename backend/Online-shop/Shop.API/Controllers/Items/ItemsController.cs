using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.API.Common.Responses;
using Shop.API.Controllers.Items.DTO;
using Shop.API.Controllers.Items.DTO.InputDto;
using Shop.API.Controllers.Items.Handlers;
using Shop.API.Controllers.Items.Operations;

namespace Shop.API.Controllers.Items
{
    public class ItemsController : BaseController
    {
        public ItemsController(
            IMediator mediator,
            IMapper mapper)
            : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Get items list.
        /// </summary>
        [HttpGet(Routes.Items.GetItems)]
        [ProducesResponseType(type: typeof(PageDto<ShopItemListEntryDto>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemsAsync(
            [FromQuery] GetItems.GetItemsRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();
        }

        /// <summary>
        /// Get item's details by it's id.
        /// </summary>
        [HttpGet(Routes.Items.GetItemById)]
        [ProducesResponseType(type: typeof(DetailedItemDto), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemByIdAsync([FromQuery] GetItemDetailed.GetItemDetailedRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();
        }

        /// <summary>
        /// Delete item by Id.
        /// </summary>
        [HttpDelete(Routes.Items.DeleteItemById)]
        public async Task<IActionResult> DeleteItemByIdAsync([FromQuery] DeleteItem.DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();
        }

        /// <summary>
        /// Create item.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost(Routes.Items.CreateItem)]
        [ProducesResponseType(type: typeof(DetailedItemDto), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateItemAsync([FromQuery] CreateItem.CreateItemRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();
        }

        [HttpPatch(Routes.Items.UpdateItemById)]
        [ProducesResponseType(type: typeof(ItemDto), statusCode: StatusCodes.Status201Created)]
        public async Task<IActionResult> PatchUpdateItemAsync([FromQuery] UpdateItem.UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();
        }
    }
}
