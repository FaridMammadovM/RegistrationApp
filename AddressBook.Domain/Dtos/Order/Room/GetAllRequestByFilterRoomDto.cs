namespace AddressBook.Domain.Dtos.Order.Room
{
    public sealed class GetAllRequestByFilterRoomDto
    {
        public int? RoomId { get; set; }

        public DateTime? StartDay { get; set; }

        public DateTime? EndDay { get; set; }
        public int Id { get; set; }
        public string Userrole { get; set; }
    }
}
