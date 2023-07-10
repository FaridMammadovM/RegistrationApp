using AddressBook.Domain.Dtos.Car;
using AddressBook.Infrastructure.Repositories.CarRepository;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace AddressBook.Api.Application.Queries.CarQuery.GetById
{
    public sealed class CarGetByIdQueryHandler : IRequestHandler<CarGetByIdQuery, CarByIdDto>
    {
        private readonly ICarRepository carRepository;
        private readonly IValidator<CarGetByIdQuery> _validator;
        private readonly IMapper _mapper;


        public CarGetByIdQueryHandler(ICarRepository carRepository, IValidator<CarGetByIdQuery> validator, IMapper mapper)
        {
            this.carRepository = carRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<CarByIdDto> Handle(CarGetByIdQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            CarByIdDto result = new();
            var res = carRepository.GetById(request.Id).Result;
            if (res != null)
            {
                CarByIdDto department = _mapper.Map<CarByIdDto>(res);
                result = department;
            }
            return await Task.FromResult(result);

        }
    }
}