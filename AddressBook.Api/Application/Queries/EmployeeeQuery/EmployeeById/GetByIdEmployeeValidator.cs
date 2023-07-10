using FluentValidation;

namespace AddressBook.Api.Application.Queries.EmployeeeQuery.EmployeeById
{
    public class GetByIdEmployeeValidator : AbstractValidator<GetByIdEmployeeQuery>
    {
        public GetByIdEmployeeValidator()
        {
            RuleFor(v => v.Id).NotEqual(0).NotNull().NotEmpty().WithMessage("Id cannot be null");
        }
    }
}
