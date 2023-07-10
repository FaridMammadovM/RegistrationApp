using AddressBook.Domain.Dtos.Employee;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeGetAllFilter
{
    public class GetEmployeeFilterQuery : IRequest<List<EmployeeGetAllReturnDto>>
    {
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public int? PositionId { get; set; }
        public string? Floor { get; set; }
        public string? RoomNumber { get; set; }
        public int? DepartmentId { get; set; }
        public string? KeyWord { get; set; }

    }
}
