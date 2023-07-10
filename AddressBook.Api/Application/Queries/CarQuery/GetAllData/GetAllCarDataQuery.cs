using AddressBook.Domain.Dtos.Car;
using MediatR;

namespace AddressBook.Api.Application.Queries.CarQuery.GetAllData
{
    public class GetAllCarDataQuery : IRequest<List<GetAllCarDataDto>>
    {
    }
}
