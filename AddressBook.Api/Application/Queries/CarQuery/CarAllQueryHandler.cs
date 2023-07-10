using AddressBook.Domain.Dtos.Car;
using AddressBook.Infrastructure.Repositories.CarRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.CarQuery
{
    public class CarAllQueryHandler : IRequestHandler<CarAllQuery, List<CarDto>>
    {
        private readonly ICarRepository carRepository;
        private readonly IValidator<CarAllQuery> _validator;
        private readonly IMapper _mapper;


        public CarAllQueryHandler(ICarRepository carRepository, IValidator<CarAllQuery> validator, IMapper mapper)
        {
            this.carRepository = carRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<List<CarDto>> Handle(CarAllQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var result = carRepository.GetAllCar().Result;

            return await Task.FromResult(result);

        }
    }
}