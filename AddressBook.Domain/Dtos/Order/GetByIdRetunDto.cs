namespace AddressBook.Domain.Dtos.Order
{
    public sealed class GetByIdRetunDto
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? PassengerType { get; set; }
        public int? EmployeeUsingId { get; set; }
        public int? DepartureType { get; set; }
        public DateTime DepartureTimeDay { get; set; }
        public DateTime ReturnTimeDay { get; set; }
        public string DepartureTimeHours { get; set; }
        public string ReturnTimeHours { get; set; }
        public int? Direction { get; set; }
        public int? PassengerCount { get; set; }
        public int? Luggage { get; set; }
        public string? LuggageSize { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public int? CarId { get; set; }
        public int? DriverId { get; set; }
        public string? RejectionReason { get; set; }
        public int? Status { get; set; }
    }
}
