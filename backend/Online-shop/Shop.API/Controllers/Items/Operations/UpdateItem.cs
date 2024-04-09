using AutoMapper;
using Core.Classes.Items;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.API.Controllers.Items.DTO.InputDto;
using Shop.Services.Interfaces;

namespace Shop.API.Controllers.Items.Operations
{
    public class UpdateItem
    {
        public class UpdateItemCommand : BaseRequest
        {
            [FromRoute]
            public int ItemId { get; set; }

            [FromBody]
            public CreateItemDto Item { get; set; }
        }

        public class Handler : HandlerBase<UpdateItemCommand>
        {
            private readonly IItemsService _itemsService;

            public Handler(
                IMapper mapper,
                IItemsService itemsService)
                : base(mapper)
            {
                _itemsService = itemsService;
            }

            public override async Task<OperationResult> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
            {
                var input = ComposeInput(request.Item);
                var result = await _itemsService.UpdateItemByIdAsync(request.ItemId, input, cancellationToken);

                return result.AsOperationResult();
            }

            private ShopItem ComposeInput(CreateItemDto item)
            {
                return new ShopItem
                {
                    Name = item.Name,
                    Description = item.Description,
                };
            }
        }
    }
}
