using AddressBook.Infrastructure.Repositories.CarRepository;
using FluentValidation;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace AddressBook.Api.Application.Commands.CarCommand.Delete
{
    public sealed class CarDeleteCommandHandler : IRequestHandler<CarDeleteCommand, int>
    {
        private readonly ICarRepository carRepository;
        private readonly IValidator<CarDeleteCommand> _validator;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CarDeleteCommandHandler(ICarRepository carRepository, IValidator<CarDeleteCommand> validator, IHttpContextAccessor httpContextAccessor)
        {
            this.carRepository = carRepository;
            _validator = validator;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> Handle(CarDeleteCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(authorizationHeader);

            var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            var insertUser = claims["Username"];
            var res = carRepository.Delete(request.Id, insertUser).Result;

            return res;

        }
    }
}
