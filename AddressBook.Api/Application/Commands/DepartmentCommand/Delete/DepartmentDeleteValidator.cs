using FluentValidation;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Delete
{
    public class DepartmentDeleteValidator : AbstractValidator<DepartmentDeleteCommand>
    {
        public DepartmentDeleteValidator()
        {

            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("Name cannot be null");

        }
    }
}
