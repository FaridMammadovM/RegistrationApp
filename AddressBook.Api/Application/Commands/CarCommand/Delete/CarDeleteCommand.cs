using MediatR;

namespace AddressBook.Api.Application.Commands.CarCommand.Delete
{
    public class CarDeleteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
