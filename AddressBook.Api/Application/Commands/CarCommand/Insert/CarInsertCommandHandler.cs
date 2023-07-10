using AddressBook.Domain.Models;
using AddressBook.Infrastructure.Repositories.CarRepository;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace AddressBook.Api.Application.Commands.CarCommand.Insert
{
    public class CarInsertCommandHandler : IRequestHandler<CarInsertCommand, int?>
    {
        private readonly ICarRepository carRepository;
        private readonly IValidator<CarInsertCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CarInsertCommandHandler(ICarRepository carRepository, IValidator<CarInsertCommand> validator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.carRepository = carRepository;
            _validator = validator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> Handle(CarInsertCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(authorizationHeader);

            var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            Car model = _mapper.Map<Car>(request);
            model.InsertUser = claims["Username"];

            int? id = await carRepository.InsertCar(model);
            return id;
        }
    }
}
