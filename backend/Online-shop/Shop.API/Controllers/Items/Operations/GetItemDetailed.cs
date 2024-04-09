using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.API.Controllers.Items.DTO;
using Shop.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Shop.API.Controllers.Items.Operations
{
    public class GetItemDetailed
    {
        public class GetItemDetailedRequest : BaseRequest
        {
            /// <summary>
            /// Item's id.
            /// </summary>
            [Required]
            [FromRoute]
            public int ItemId { get; set; }
        }

        public class Handler : HandlerBase<GetItemDetailedRequest>
        {
            private readonly IItemsService _itemsService;

            public Handler(
                IMapper mapper,
                IItemsService itemsService)
                : base(mapper)
            {
                _itemsService = itemsService;
            }

            public override async Task<OperationResult> Handle(GetItemDetailedRequest request, CancellationToken cancellationToken)
            {
                var result = await _itemsService.GetItemDetailedAsync(request.ItemId, cancellationToken);
                var response = _mapper.Map<DetailedItemDto>(result);

                return response.AsOperationResult();
            }
        }
    }
}
