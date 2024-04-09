using AutoMapper;
using Shop.API.Common.Responses;
using Shop.Services.Common;

namespace Shop.API.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PaginationContext, PaginationContextDto>();
        }
    }
}
