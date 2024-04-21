using AutoMapper;
using Core.Classes.Items;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.API.Controllers.Items.DTO;
using Shop.API.Controllers.Items.DTO.InputDto;
using Shop.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Shop.API.Controllers.Items.Operations
{
    public class CreateItem
    {
        public class CreateItemRequest : BaseRequest
        {
            [FromForm]
            [Required]
            public CreateItemDto Item { get; set; }
        }

        public class Handler : HandlerBase<CreateItemRequest>
        {
            private readonly IItemsService _itemsService;

            public Handler(
                IMapper mapper,
                IItemsService itemsService)
                : base(mapper)
            {
                _itemsService = itemsService;
            }

            public override async Task<OperationResult> Handle(CreateItemRequest request, CancellationToken cancellationToken)
            {
                var input = ComposeInput(request.Item);
                var result = await _itemsService.CreateItemAsync(input, cancellationToken);

                var response = _mapper.Map<DetailedItemDto>(result);
                return response.AsOperationResult();
            }

            private CreateShopItem ComposeInput(CreateItemDto request)
            {
                return new CreateShopItem
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    ImageStreams = request.Images.Select(x => x.OpenReadStream()).ToArray(),
                    Category = request.Category
                };
            }
        }
    }
}
