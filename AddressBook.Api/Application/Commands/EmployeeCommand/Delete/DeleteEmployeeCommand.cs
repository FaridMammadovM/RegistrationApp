using MediatR;

namespace AddressBook.Api.Application.Commands.EmployeeCommand.Delete
{
    public class DeleteEmployeeCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}

