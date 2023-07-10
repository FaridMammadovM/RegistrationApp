using AddressBook.Api.Application.Commands.OrderCommand.ConfirmRoom;
using AddressBook.Api.Application.Commands.OrderCommand.InsertRoom;
using AddressBook.Api.Application.Commands.OrderCommand.UpdateRoom;
using AddressBook.Api.Application.Queries.OrderQuery.RoomGetAllByFilter;
using AddressBook.Domain.Dtos.Order.Room;
using AddressBook.Domain.Dtos.Room;
using AddressBook.Domain.Models.Room;
using AutoMapper;

namespace AddressBook.Api.Infrastructure.MappingProfiles
{
    public sealed class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<GetAllRoomDto, RoomModel>().ReverseMap();
            CreateMap<InsertOrderRoomCommand, RoomRegistrationModel>().ReverseMap();
            CreateMap<UpdateOrderRoomCommand, RoomRegistrationModel>().ReverseMap();
            CreateMap<GetByIdRetunRoomDto, RoomRegistrationModel>().ReverseMap();
            CreateMap<RoomRegistrationModel, RoomConfirmOrderCommand>().ForMember(dest => dest.Status, opt =>
            {
                opt.MapFrom(src => src.Status == "Tədiqləndi" ? 1 : src.Status == "İmtina edildi" ? 2 : 0);
            })
            .ReverseMap();

            CreateMap<RoomConfirmOrderCommand, RoomRegistrationModel>()
               .ForMember(dest => dest.Status, opt =>
               {
                   opt.MapFrom(src => src.Status == 1 ? "Təsdiqləndi" : src.Status == 2 ? "İmtina edildi" : null);
               })
           .ReverseMap();

            CreateMap<GetAllRequestByFilterRoomDto, GetAllByFilterRoomOrderQuery>().ReverseMap();

        }
    }
}
