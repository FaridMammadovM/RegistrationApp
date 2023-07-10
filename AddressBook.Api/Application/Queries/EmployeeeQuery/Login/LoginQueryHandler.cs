using AddressBook.Domain.Dtos.Employee;
using AddressBook.Infrastructure.Repositories.EmployeRepository;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginReturnDto>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IValidator<LoginQuery> _validator;
        private readonly IMapper _mapper;
        public LoginQueryHandler(IEmployeeRepository employeeRepository, IValidator<LoginQuery> validator, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            _validator = validator;
            _mapper = mapper;

        }
        public async Task<LoginReturnDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            LoginDto login = _mapper.Map<LoginDto>(request);
            LoginReturnDto result = new();
            var res = employeeRepository.GetLogin(login).Result;
            if (res.Id > 0)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Id", res.Id.ToString()));
                claims.Add(new Claim("Firstname", res.Firstname));
                claims.Add(new Claim("Surname", res.Surname));
                claims.Add(new Claim("Patronymic", res.Patronymic));
                claims.Add(new Claim("Username", res.UserName));
                claims.Add(new Claim("Userrole", res.UserRole.ToString()));

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.Key));
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(8),
                    SigningCredentials = credentials,
                    Audience = request.Audience,
                    Issuer = request.Issuer
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var resutlToken = tokenHandler.WriteToken(token);
                result = _mapper.Map<LoginReturnDto>(res);

                result.Token = resutlToken;
            }
            return await Task.FromResult(result);

        }
    }
}
