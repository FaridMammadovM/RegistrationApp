namespace AddressBook.Domain.Dtos.Order.Room
{
    public sealed class GetByIdRetunRoomDto
    {
        public int Id { get; set; }
        public string MeetName { get; set; }

        public int ParticipantCount { get; set; }
        public DateTime? Time { get; set; }

        public TimeSpan? StartHours { get; set; }

        public TimeSpan? EndHours { get; set; }
        public int RoomId { get; set; }
        public string Note { get; set; }
        public string? RejectReason { get; set; }
        public string Status { get; set; }
        public int InsertBy { get; set; }



    }
}
