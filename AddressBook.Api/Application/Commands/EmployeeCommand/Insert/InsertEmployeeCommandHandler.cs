using AddressBook.Domain.Models.Employee;
using AddressBook.Infrastructure.Repositories.EmployeRepository;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace AddressBook.Api.Application.Commands.EmployeeCommand.Insert
{
    public class InsertEmployeeCommandHandler : IRequestHandler<InsertEmployeeCommand, int?>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IValidator<InsertEmployeeCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public InsertEmployeeCommandHandler(IEmployeeRepository employeeRepository, IValidator<InsertEmployeeCommand> validator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> Handle(InsertEmployeeCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(authorizationHeader);

            var claims = decodedToken.Claims.ToDictionary(c => c.Type, c => c.Value);

            Employee model = _mapper.Map<Employee>(request);
            model.InsertUser = claims["Username"];
            int? id = await employeeRepository.InsertEmployee(model);
            return id;

        }
    }
}
