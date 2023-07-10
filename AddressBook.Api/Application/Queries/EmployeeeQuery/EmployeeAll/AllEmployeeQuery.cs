using AddressBook.Domain.Dtos.Employee;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeAll
{
    public class AllEmployeeQuery : IRequest<List<EmployeeAllReturnDto>>
    {
    }
}
