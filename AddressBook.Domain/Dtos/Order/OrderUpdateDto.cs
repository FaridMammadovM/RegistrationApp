namespace AddressBook.Domain.Dtos.Order
{
    public sealed class OrderUpdateDto
    {
        public int Id { get; set; }

        public int PassengerType { get; set; }

        public int? EmployeeUsingId { get; set; }

        public int DepartureType { get; set; }

        public DateTime? DepartureTimeDay { get; set; }

        public DateTime? ReturnTimeDay { get; set; }

        public TimeSpan? DepartureTimeHours { get; set; }

        public TimeSpan? ReturnTimeHours { get; set; }

        public int Direction { get; set; }

        public int PassengerCount { get; set; }

        public int Luggage { get; set; }

        public string? LuggageSize { get; set; }

        public string Address { get; set; }

        public string? Note { get; set; }
    }
}
