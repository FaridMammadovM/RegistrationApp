using MediatR;

namespace AddressBook.Api.Application.Commands.CarCommand.Update
{
    public class CarUpdateCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Model { get; set; }

        public string Brand { get; set; }

        public string Number { get; set; }

        public int Seat { get; set; }

        public string Kind { get; set; }

        public string Color { get; set; }

        public string Year { get; set; }
    }
}
