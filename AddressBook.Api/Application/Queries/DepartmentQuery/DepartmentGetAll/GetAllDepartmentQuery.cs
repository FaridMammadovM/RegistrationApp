using AddressBook.Domain.Dtos.Department;
using MediatR;

namespace AddressBook.Api.Application.Queries.DepartmentQuery.DepartmentGetAll
{
    public class GetAllDepartmentQuery : IRequest<List<DepartmentGetAllDto>>
    {

    }
}
