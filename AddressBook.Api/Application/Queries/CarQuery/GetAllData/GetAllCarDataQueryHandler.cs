using AddressBook.Domain.Dtos.Car;
using AddressBook.Infrastructure.Repositories.CarRepository;
using MediatR;

namespace AddressBook.Api.Application.Queries.CarQuery.GetAllData
{
    public class GetAllCarDataQueryHandler : IRequestHandler<GetAllCarDataQuery, List<GetAllCarDataDto>>
    {
        private readonly ICarRepository carRepository;
        public GetAllCarDataQueryHandler(ICarRepository carRepository)
        {
            this.carRepository = carRepository;

        }
        public async Task<List<GetAllCarDataDto>> Handle(GetAllCarDataQuery request, CancellationToken cancellationToken)
        {
            var result = carRepository.GetAll();
            return await Task.FromResult(result.Result);

        }
    }
}
