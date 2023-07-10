using AddressBook.Domain.Dtos.Car;
using MediatR;

namespace AddressBook.Api.Application.Queries.CarQuery
{
    public class CarAllQuery : IRequest<List<CarDto>>
    {
    }
}
