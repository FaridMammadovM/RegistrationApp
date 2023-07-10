using FluentValidation;

namespace AddressBook.Api.Application.Commands.DepartmentCommand.Insert
{
    public class DepartmentInsertValidator : AbstractValidator<DepartmentInsertCommand>
    {
        public DepartmentInsertValidator()
        {

            RuleFor(v => v.Name).NotNull().NotEmpty().WithMessage("Name cannot be null");
            RuleFor(v => v.ParentId).GreaterThan(0).WithMessage("ParentId should be greater than 0");
        }
    }
}
