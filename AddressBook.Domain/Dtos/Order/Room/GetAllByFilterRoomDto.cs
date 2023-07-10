namespace AddressBook.Domain.Dtos.Order.Room
{
    public sealed class GetAllByFilterRoomDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string RegistrationNo { get; set; }
        public string RoomName { get; set; }
        public string RegistrationTime { get; set; }
        public string Fullname { get; set; }
        public string Meetingname { get; set; }
        public string Begintime { get; set; }
        public string Endtime { get; set; }
    }
}
