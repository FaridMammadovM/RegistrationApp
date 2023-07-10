using AddressBook.Domain.Dtos.Department;
using MediatR;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Delete
{
    public class DepartmentDeleteCommand : IRequest<DepartmentIdDto>
    {
        public long Id { get; set; }
    }
}
