using AddressBook.Domain.Dtos.Department;
using MediatR;

namespace AddressBook.Api.Application.Queries.DepartmentQuery.DepartmentById
{
    public class GetByIdDepartmentQuery : IRequest<DepartmentIdDto>
    {
        public long Id { get; set; }
    }
}
