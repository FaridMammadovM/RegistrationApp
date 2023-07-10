namespace AddressBook.Domain.Dtos.Order
{
    public sealed class OrderGetAllDto
    {
        public int? EmployeeId { get; set; }

        public string? PassengerType { get; set; }

        public int? EmployeeUsingId { get; set; }

        public string? DepartureType { get; set; }

        public DateTime? DepartureTimeDay { get; set; }

        public DateTime? ReturnTimeDay { get; set; }

        public TimeSpan? DepartureTimeHours { get; set; }

        public TimeSpan? ReturnTimeHours { get; set; }

        public string? Direction { get; set; }

        public int? CarId { get; set; }

        public int? DriverId { get; set; }

        public string? Status { get; set; }

        public int? Id { get; set; }
        public string? Userrole { get; set; }
    }
}
