using AddressBook.Domain.Dtos.Room;
using MediatR;

namespace AddressBook.Api.Application.Queries.RoomQuery.GetByRoomIdWithEquipment
{
    public class GetByRoomIdWithEquipmentQuery : IRequest<GetByIdRoomWithEpiuqmentDto>
    {
        public int Id { get; set; }
    }
}
