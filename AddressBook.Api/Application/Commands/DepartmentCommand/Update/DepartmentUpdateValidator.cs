using FluentValidation;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Update
{
    public class DepartmentUpdateValidator : AbstractValidator<DepartmentUpdateCommand>
    {
        public DepartmentUpdateValidator()
        {
            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("Name cannot be null")
                .GreaterThan(0).WithMessage("ParentId should be greater than 0");
            RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name cannot be null");
            RuleFor(v => v.ParentId).GreaterThan(0).WithMessage("ParentId should be greater than 0");
        }
    }
}
