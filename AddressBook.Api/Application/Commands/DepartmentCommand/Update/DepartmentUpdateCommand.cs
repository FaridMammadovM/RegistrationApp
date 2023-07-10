using MediatR;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Update
{
    public class DepartmentUpdateCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
