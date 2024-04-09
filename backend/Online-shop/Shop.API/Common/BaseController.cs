using AutoMapper;
using Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Shop.API.Common
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        public BaseController(IMediator mediator, IMapper mapper)
            : base()
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }

    public static class ActionResultExtensions
    {
        public static IActionResult AsActionResult(this object result) => new OkObjectResult(result);
        public static IActionResult AsActionResult(this OperationResult result) 
        {
            if (result.Error is null) 
            {
                return new OkObjectResult(result.Result);
            }
            else
            {
                var error = result.Error;
                var responseBody = new HttpExceptionResponse
                {
                    Message = error.Message,
                    StackTrace = error.StackTrace
                };

                if (error is { } && error is AppException)
                {
                    responseBody.StatusCode = 400;
                    return new BadRequestObjectResult(responseBody);
                }
                else
                {
                    responseBody.StatusCode = 500;
                    return new ObjectResult(responseBody)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
        } 
    }
}
