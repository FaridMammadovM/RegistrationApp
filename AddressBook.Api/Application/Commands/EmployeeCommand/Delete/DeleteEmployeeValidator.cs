using FluentValidation;

namespace AddressBook.Api.Application.Commands.EmployeeCommand.Delete
{
    public class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeValidator()
        {
            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("Name cannot be null")
                                .GreaterThan(0).WithMessage("EmployeeId should be greater than 0");

        }
    }
}
