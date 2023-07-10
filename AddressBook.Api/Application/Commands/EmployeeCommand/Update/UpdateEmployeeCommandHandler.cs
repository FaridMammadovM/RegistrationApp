using AddressBook.Domain.Models.Employee;
using AddressBook.Infrastructure.Repositories.EmployeRepository;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace AddressBook.Api.Application.Commands.EmployeeCommand.Update
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IValidator<UpdateEmployeeCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<UpdateEmployeeCommand> validator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(authorizationHeader);

            var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            var employee = _mapper.Map<Employee>(request);
            employee.UpdateUser = claims["Username"];
            var res = employeeRepository.UpdateEmployee(employee).Result;
            return res;


        }
    }
}