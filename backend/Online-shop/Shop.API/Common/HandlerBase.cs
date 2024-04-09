using AutoMapper;
using MediatR;

namespace Shop.API.Common
{
    public abstract class HandlerBase<T> : IRequestHandler<T, OperationResult>
        where T : BaseRequest
    {
        protected HandlerBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected readonly IMapper _mapper;

        public abstract Task<OperationResult> Handle(T request, CancellationToken cancellationToken);
    }

    public static class HandlerExtensions
    {
        public static OperationResult AsOperationResult(this object operationResult)
        {
            return new OperationResult
            {
                Result = operationResult
            };
        }
    }
}
