using MediatR;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Insert
{
    public class DepartmentInsertCommand : IRequest<long?>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
