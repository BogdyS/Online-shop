using AutoMapper;
using Shop.API.Common;
using Shop.API.Controllers.Categories.DTO;
using Shop.Services.Interfaces;

namespace Shop.API.Controllers.Categories.Operations
{
    public class GetCategories
    {
        public class GetCategoriesRequest : BaseRequest
        {
        }

        public class Handler : HandlerBase<GetCategoriesRequest>
        {
            private readonly ICategoriesService _categoriesService;

            public Handler(
                ICategoriesService categoriesService,
                IMapper mapper) : base(mapper)
            {
                _categoriesService = categoriesService;
            }

            public override async Task<OperationResult> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
            {
                var result = await _categoriesService.GetCategoriesAsync(cancellationToken);

                var response = _mapper.Map<List<CategoriesResponseDto>>(result);

                return response.AsOperationResult();
            }
        }
    }
}
