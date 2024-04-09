using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Common;
using Shop.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Shop.API.Controllers.Search.Operations
{
    public class GetSearchAutoComplete
    {
        public class GetSearchAutoCompleteRequest : BaseRequest
        {
            [Required]
            [FromQuery]
            public string Search { get; set; }
        }

        public class Handler : HandlerBase<GetSearchAutoCompleteRequest>
        {
            private readonly ISearchService _searchService;
            public Handler(
                IMapper mapper,
                ISearchService searchService)
                : base(mapper)
            {
                _searchService = searchService;
            }

            public override async Task<OperationResult> Handle(GetSearchAutoCompleteRequest request, CancellationToken cancellationToken)
            {
                var result = await _searchService.GetAutoCompleteItemNames(request.Search, cancellationToken);

                return (new { Hints = result }).AsOperationResult();
            }
        }
    }
}
