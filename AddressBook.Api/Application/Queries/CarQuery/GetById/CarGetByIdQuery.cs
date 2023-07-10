using AddressBook.Domain.Dtos.Car;
using MediatR;

namespace AddressBook.Api.Application.Queries.CarQuery.GetById
{
    public class CarGetByIdQuery : IRequest<CarByIdDto>
    {
        public int Id { get; set; }
    }
}
