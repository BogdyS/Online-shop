using AutoMapper;
using Core.Classes.Items;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.API.Common.Requests;
using Shop.API.Common.Responses;
using Shop.API.Controllers.Items.DTO.InputDto;
using Shop.Services.Interfaces;
using Shop.Services.Models;

namespace Shop.API.Controllers.Items.Handlers
{
    public class GetItems
    {
        public class GetItemsRequest : PaginatedRequest
        {
            /// <summary>
            /// Search string (search in name and description).
            /// </summary>
            [FromQuery]
            public string Search { get; set; }

            /// <summary>
            /// Min price.
            /// </summary>
            [FromQuery]
            public int? MinPrice { get; set; }

            /// <summary>
            /// Max price.
            /// </summary>
            [FromQuery]
            public int? MaxPrice { get; set; }

            [FromQuery]
            public List<int> CategoriesIds { get; set; } = new();
        }

        public class GetItemsHandler : HandlerBase<GetItemsRequest>
        {
            private readonly IItemsService _itemsService;

            public GetItemsHandler(
                IMapper mapper,
                IItemsService itemsService) : base(mapper)
            {
                _itemsService = itemsService;
            }

            public override async Task<OperationResult> Handle(GetItemsRequest request, CancellationToken cancellationToken)
            {
                var result = await _itemsService.GetItemsAsync(
                    new GetItemsRequestModel
                    {
                        Pagination = request.ToPaginationRequest(),
                        Search = request.Search,
                        MinPrice = request.MinPrice,
                        MaxPrice = request.MaxPrice,
                        CategoriesIds = request.CategoriesIds
                    },
                    cancellationToken);

                var response = PageExtensions
                    .MapFromPaginatedResponse<ShopItemListEntry, ShopItemListEntryDto>(result, _mapper);

                return response.AsOperationResult();
            }
        }
    }
}
