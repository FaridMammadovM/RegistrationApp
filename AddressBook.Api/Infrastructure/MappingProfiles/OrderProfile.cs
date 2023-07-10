using AddressBook.Api.Application.Commands.OrderCommand.Confirm;
using AddressBook.Api.Application.Commands.OrderCommand.Insert;
using AddressBook.Api.Application.Commands.OrderCommand.Update;
using AddressBook.Api.Application.Queries.OrderQuery.GetAllFilter;
using AddressBook.Domain.Dtos.Order;
using AddressBook.Domain.Models;
using AutoMapper;

namespace AddressBook.Api.Infrastructure.MappingProfiles
{
    public sealed class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderInsertDto>().ReverseMap();
            CreateMap<Order, OrderConfirmDto>().ForMember(dest => dest.Status, opt =>
            {
                opt.MapFrom(src => src.Status == "Tədiqləndi" ? 1 : src.Status == "İmtina edildi" ? 2 : 0);
            })
            .ReverseMap();
            CreateMap<Order, OrderGetAllDto>().ReverseMap();

            CreateMap<Order, GetByIdRetunDto>()
                .ForMember(dest => dest.PassengerType, opt =>
                {
                    opt.MapFrom(src => src.PassengerType == "Əməkdaş" ? 1 : src.PassengerType == "Qonaq" ? 2 : 0);
                })
                 .ForMember(dest => dest.DepartureType, opt =>
                 {
                     opt.MapFrom(src => src.DepartureType == "Şəhər daxili" ? 1 : src.DepartureType == "Ezamiyyət" ? 2 : 0);
                 })
                .ForMember(dest => dest.Direction, opt =>
                {
                    opt.MapFrom(src => src.Direction == "Gediş" ? 1 : src.Direction == "Gediş-gəliş" ? 2 : 0);
                })
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status == "Təsdiqləndi" ? 1 : src.Status == "İmtina edildi" ? 2 : src.Status == "Gözləmədə" ? 3 : 0);
                })
                .ForMember(dest => dest.DepartureTimeHours, opt =>
                {
                    opt.MapFrom(src => (src.DepartureTimeDay + src.DepartureTimeHours).ToString("dd.MM.yyyy HH:mm"));
                })
                .ForMember(dest => dest.ReturnTimeHours, opt =>
                {
                    opt.MapFrom(src => (src.ReturnTimeDay + src.ReturnTimeHours).ToString("dd.MM.yyyy HH:mm"));
                })
                .ReverseMap();

            CreateMap<UpdateOrderCommand, Order>()
               .ForMember(dest => dest.PassengerType, opt =>
               {
                   opt.MapFrom(src => src.PassengerType == 1 ? "Əməkdaş" : src.PassengerType == 2 ? "Qonaq" : null);
               })
                 .ForMember(dest => dest.DepartureType, opt =>
                 {
                     opt.MapFrom(src => src.DepartureType == 1 ? "Şəhər daxili" : src.DepartureType == 2 ? "Ezamiyyət" : null);
                 })
                .ForMember(dest => dest.Direction, opt =>
                {
                    opt.MapFrom(src => src.Direction == 1 ? "Gediş" : src.Direction == 2 ? "Gediş-gəliş" : null);
                })
           .ReverseMap();

            CreateMap<OrderInsertCommand, Order>()
                .ForMember(dest => dest.PassengerType, opt =>
                {
                    opt.MapFrom(src => src.PassengerType == 1 ? "Əməkdaş" : src.PassengerType == 2 ? "Qonaq" : null);
                })
                .ForMember(dest => dest.DepartureType, opt =>
                {
                    opt.MapFrom(src => src.DepartureType == 1 ? "Şəhər daxili" : src.DepartureType == 2 ? "Ezamiyyət" : null);
                })
                .ForMember(dest => dest.Direction, opt =>
                {
                    opt.MapFrom(src => src.Direction == 1 ? "Gediş" : src.Direction == 2 ? "Gediş-gəliş" : null);
                })
            .ReverseMap();
            CreateMap<ConfirmOrderCommand, Order>()
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status == 1 ? "Təsdiqləndi" : src.Status == 2 ? "İmtina edildi" : null);
                })
            .ReverseMap();

            CreateMap<GetAllByFilterOrderQuery, OrderGetAllDto>()
                 .ForMember(dest => dest.PassengerType, opt =>
                 {
                     opt.MapFrom(src => src.PassengerType == 1 ? "Əməkdaş" : src.PassengerType == 2 ? "Qonaq" : null);
                 })
                 .ForMember(dest => dest.DepartureType, opt =>
                 {
                     opt.MapFrom(src => src.DepartureType == 1 ? "Şəhər daxili" : src.DepartureType == 2 ? "Ezamiyyət" : null);
                 })
                .ForMember(dest => dest.Direction, opt =>
                {
                    opt.MapFrom(src => src.Direction == 1 ? "Gediş" : src.Direction == 2 ? "Gediş-gəliş" : null);
                })
                .ForMember(dest => dest.Status, opt =>
                {
                    opt.MapFrom(src => src.Status == 1 ? "Təsdiqləndi" : src.Status == 2 ? "İmtina edildi" : src.Status == 3 ? "Gözləmədə" : null);
                })
            .ReverseMap();

        }
    }
}
