using MediatR;

namespace AddressBook.Api.Application.Commands.OrderCommand.Insert
{
    public class OrderInsertCommand : IRequest<long?>
    {
        public int EmployeeId { get; set; }

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
        public string? InsertUser { get; set; }
        public string? MailFrom { get; set; }
        public string? Password { get; set; }
    }
}
