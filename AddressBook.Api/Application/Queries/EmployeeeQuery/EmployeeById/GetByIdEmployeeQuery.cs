using AddressBook.Domain.Dtos.Employee;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeById
{
    public class GetByIdEmployeeQuery : IRequest<EmployeeIdDto>
    {
        public int Id { get; set; }
    }
}
