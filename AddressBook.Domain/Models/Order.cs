using DapperExtension;

namespace AddressBook.Domain.Models
{
    [Table(TableName = "\"AB_CAR\".registration", KeyName = "id", KeyNameInClass = "Id", IsIdentity = true)]
    public class Order : BaseEntity
    {
        [Column(Name = "employee_id")]
        public int EmployeeId { get; set; }

        [Column(Name = "pasenger_kind")]
        public string PassengerType { get; set; }

        [Column(Name = "employee_using_id")]
        public int EmployeeUsingId { get; set; }

        [Column(Name = "type_of_departure")]
        public string DepartureType { get; set; }

        [Column(Name = "departure_time")]
        public DateTime DepartureTimeDay { get; set; }

        [Column(Name = "return_time")]
        public DateTime ReturnTimeDay { get; set; }

        [Column(Name = "departure_hour")]
        public TimeSpan DepartureTimeHours { get; set; }

        [Column(Name = "return_hour")]
        public TimeSpan ReturnTimeHours { get; set; }

        [Column(Name = "direction")]
        public string Direction { get; set; }

        [Column(Name = "passenger_count")]
        public int PassengerCount { get; set; }

        [Column(Name = "cargo")]
        public int Luggage { get; set; }

        [Column(Name = "cargo_size")]
        public string LuggageSize { get; set; }

        [Column(Name = "address_list")]
        public string Address { get; set; }

        [Column(Name = "note")]
        public string Note { get; set; }

        [Column(Name = "car_id")]
        public int? CarId { get; set; }

        [Column(Name = "driver_id")]
        public int? DriverId { get; set; }

        [Column(Name = "reject_reason")]
        public string? RejectionReason { get; set; }

        [Column(Name = "status")]
        public string Status { get; set; }

    }
}
