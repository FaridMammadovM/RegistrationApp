using AddressBook.Domain.Dtos.Room;
using MediatR;

namespace AddressBook.Api.Application.Queries.RoomQuery
{
    public class GetAllRoomQuery : IRequest<List<GetAllRoomDto>>
    {
    }
}
