using AutoMapper;
using MeatManager.Model.Entities;
using MeatManager.Model.ValueObjects;
using MeatManager.Service.DTOs;

namespace MeatManager.Service.Mapping
{
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<Buyer, BuyerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document.Value))
                .ForMember(dest => dest.Address, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address = new AddressDto
                    {
                        CityId = src.Address?.CityId.ToString(),
                        StateId = src.Address?.StateId.ToString(),
                    };
                });

            CreateMap<BuyerDto, Buyer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Address, opt => opt.Ignore())
                 .AfterMap((src, dest) =>
                 {
                     dest.Address = new Address
                     {
                         CityId = Guid.TryParse(src.Address.CityId, out var cityGuid) ? cityGuid : Guid.Empty,
                         StateId = Guid.TryParse(src.Address.StateId, out var stateId) ? stateId : Guid.Empty
                     };
                 });

            CreateMap<Order, OrderDto>()
                .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MeatId, opt => opt.MapFrom(src => src.Meat.Id.ToString()))
            .ForMember(dest => dest.PriceBRL, opt => opt.Ignore());

            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? Guid.Empty))
                .ForMember(dest => dest.Meat, opt => opt.MapFrom(src => new Meat { Id = Guid.Parse(src.MeatId) }));

            CreateMap<Meat, MeatDto>()
            .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Origin))
            .ReverseMap();
        }
    }
}
