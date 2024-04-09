using AutoMapper;
using Core.Classes.Items;
using Persistence.Entities;

namespace Shop.Services.MappingProfiles
{
    public class ItemsMappingProfile : Profile
    {
        public ItemsMappingProfile()
        {
            CreateMap<Item, ShopItemEntry>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ImageUrls, opt => opt.Ignore());
        }
    }
}
