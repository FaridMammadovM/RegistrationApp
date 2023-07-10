using AddressBook.Domain.Dtos.Employee;
using MediatR;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.Login
{
    public class LoginQuery : IRequest<LoginReturnDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        internal string Key { get; set; }
        internal string Audience { get; set; }
        internal string Issuer { get; set; }

        public void SetJwtConfiguration(string key, string audience, string issuer)
        {
            Key = key;
            Audience = audience;
            Issuer = issuer;
        }
    }
}
