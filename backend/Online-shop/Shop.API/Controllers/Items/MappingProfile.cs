using AutoMapper;
using Core.Classes.Items;
using Shop.API.Controllers.Items.DTO;
using Shop.API.Controllers.Items.DTO.InputDto;

namespace Shop.API.Controllers.Items
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShopItemEntry, ItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<ShopItemEntry, DetailedItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ImageUrls));

            CreateMap<ShopItemListEntry, ShopItemListEntryDto>();
        }
    }
}
