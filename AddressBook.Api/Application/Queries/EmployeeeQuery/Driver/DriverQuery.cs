using AddressBook.Domain.Dtos.Employee;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.Driver
{
    public class DriverQuery : IRequest<List<DriverDto>>
    {
    }
}