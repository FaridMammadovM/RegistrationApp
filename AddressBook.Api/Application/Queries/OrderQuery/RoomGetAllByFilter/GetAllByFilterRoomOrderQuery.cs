using AddressBook.Domain.Dtos.Order.Room;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.RoomGetAllByFilter
{
    public class GetAllByFilterRoomOrderQuery : IRequest<List<GetAllByFilterRoomDto>>
    {
        public int? RoomId { get; set; }

        public DateTime? StartDay { get; set; }

        public DateTime? EndDay { get; set; }
        public int Id { get; set; }
        public string Userrole { get; set; }

    }
}
