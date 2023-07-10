using AddressBook.Domain.Models;
using AddressBook.Infrastructure.Repositories.CarRepository;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace AddressBook.Api.Application.Commands.CarCommand.Update
{
    public sealed class CarUpdateCommandHandler : IRequestHandler<CarUpdateCommand, int>
    {
        private readonly ICarRepository carRepository;
        private readonly IValidator<CarUpdateCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CarUpdateCommandHandler(ICarRepository carRepository, IValidator<CarUpdateCommand> validator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.carRepository = carRepository;
            _validator = validator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(CarUpdateCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(authorizationHeader);

            var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            var car = _mapper.Map<Car>(request);
            car.InsertUser = claims["Username"];
            var res = carRepository.UpdateCar(car).Result;
            if (res != 0)
            {
                return 1;
            }
            else
            { return 0; }


        }
    }
}
