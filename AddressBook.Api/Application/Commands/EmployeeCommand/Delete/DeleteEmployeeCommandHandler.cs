using AddressBook.Infrastructure.Repositories.EmployeRepository;
using FluentValidation;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace AddressBook.Api.Application.Commands.EmployeeCommand.Delete
{
    public sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IValidator<DeleteEmployeeCommand> _validator;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<DeleteEmployeeCommand> validator, IHttpContextAccessor httpContextAccessor)
        {
            this.employeeRepository = employeeRepository;
            _validator = validator;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(authorizationHeader);

            var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);
            var deleteUser = claims["Username"];

            var res = employeeRepository.Delete(request.Id, deleteUser).Result;
            return await Task.FromResult(res);

        }
    }
}
