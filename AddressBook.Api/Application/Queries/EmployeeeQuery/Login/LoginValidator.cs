using FluentValidation;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.Login
{
    public class LoginValidator : AbstractValidator<LoginQuery>
    {
        public LoginValidator()
        {
            RuleFor(v => v.Username).NotNull().NotEmpty().WithMessage("Email cannot be null");
            RuleFor(v => v.Password).NotNull().NotEmpty().WithMessage("Email cannot be null");
        }
    }
}

