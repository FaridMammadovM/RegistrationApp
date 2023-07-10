namespace AddressBook.Domain.Dtos.Employee
{
    public sealed class EmailDto
    {
        public string User { get; set; }
        public string UserFullname { get; set; }
        public string Userusing { get; set; }
        public string UserusingFullname { get; set; }
        public string Driver { get; set; }
        public string DriverFullname { get; set; }
        public string Status { get; set; }
        public string Reject { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan Hours { get; set; }
        public string Time { get; set; }

        public string Car { get; set; }

        public string MeetingName { get; set; }
        public string RoomName { get; set; }
    }
}
