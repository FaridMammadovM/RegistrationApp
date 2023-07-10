namespace AddressBook.Domain.Dtos.Order.Room
{
    public sealed class OrderRoomInsertDto
    {
        public string MeetName { get; set; }

        public int ParticipantCount { get; set; }
        public DateTime? Time { get; set; }

        public TimeSpan? StartHours { get; set; }

        public TimeSpan? EndHours { get; set; }
        public int RoomId { get; set; }
        public string? Note { get; set; }
    }
}
