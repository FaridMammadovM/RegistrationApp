using AddressBook.Domain.Dtos.Order.Room;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.RoomGetById
{
    public class RoomOrderGetByIdQuery : IRequest<GetByIdRetunRoomDto>
    {
        public int Id { get; set; }
    }
}

