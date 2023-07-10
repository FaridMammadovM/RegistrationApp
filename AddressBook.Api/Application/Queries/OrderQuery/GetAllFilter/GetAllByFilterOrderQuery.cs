using AddressBook.Domain.Dtos.Order;
using MediatR;

namespace AddressBook.Api.Application.Queries.OrderQuery.GetAllFilter
{
    public class GetAllByFilterOrderQuery : IRequest<List<OrderGetAllReturnDto>>
    {
        public int? EmployeeId { get; set; }

        public int? PassengerType { get; set; }

        public int? EmployeeUsingId { get; set; }

        public int? DepartureType { get; set; }

        public DateTime? DepartureTimeDay { get; set; }

        public DateTime? ReturnTimeDay { get; set; }

        public TimeSpan? DepartureTimeHours { get; set; }

        public TimeSpan? ReturnTimeHours { get; set; }

        public int? Direction { get; set; }

        public int? CarId { get; set; }

        public int? DriverId { get; set; }

        public int? Status { get; set; }

        public int? Id { get; set; }
        public string? Userrole { get; set; }
    }
}
