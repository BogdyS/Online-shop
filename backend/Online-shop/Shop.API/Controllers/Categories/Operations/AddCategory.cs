using AutoMapper;
using Core.Classes.Categories;
using Shop.API.Common;
using Shop.API.Controllers.Categories.DTO;
using Shop.Services.Interfaces;

namespace Shop.API.Controllers.Categories.Operations
{
    public class AddCategory : BaseRequest
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }

    public class Handler : HandlerBase<AddCategory>
    {
        private readonly ICategoriesService _categoriesService;

        public Handler(
            ICategoriesService categoriesService,
            IMapper mapper) : base(mapper)
        {
            _categoriesService = categoriesService;
        }

        public override async Task<OperationResult> Handle(AddCategory request, CancellationToken cancellationToken)
        {
            var input = new CreateCategoryModel
            {
                Name = request.Name,
                ImageStream = request.Image.OpenReadStream(),
            };

            var result = await _categoriesService.CreateCategoryAsync(input, cancellationToken);

            var response = _mapper.Map<CategoriesResponseDto>(result);

            return response.AsOperationResult();
        }
    }
}
