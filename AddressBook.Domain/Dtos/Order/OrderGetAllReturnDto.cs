namespace AddressBook.Domain.Dtos.Order
{
    public sealed class OrderGetAllReturnDto
    {
        public int Id { get; set; }
        public string? status { get; set; }
        public string? usagename { get; set; }
        public string? ordername { get; set; }
        public string? drivername { get; set; }
        public string? driverPhone { get; set; }
        public string? carname { get; set; }
        public string? type_of_departure { get; set; }
        public string? departure_time_str { get; set; }
        public string? return_time_str { get; set; }
        public string? departure_hour_str { get; set; }
        public string? return_hour_str { get; set; }

    }
}
