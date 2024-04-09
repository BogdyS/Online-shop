using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.Services.Interfaces;

namespace Shop.API.Controllers.Items.Operations
{
    public class DeleteItem
    {
        public class DeleteItemCommand : BaseRequest
        {
            [FromRoute]
            public int ItemId { get; set; }
        }

        public class Handler : HandlerBase<DeleteItemCommand>
        {
            private readonly IItemsService _itemService;
            public Handler(
                IMapper mapper,
                IItemsService itemService)
                : base(mapper)
            {
                _itemService = itemService;
            }

            public override async Task<OperationResult> Handle(
                DeleteItemCommand request,
                CancellationToken cancellationToken)
            {
                await _itemService.DeleteItemByIdAsync(request.ItemId, cancellationToken);

                return new OperationResult(new { Success = true });
            }
        }
    }
}
