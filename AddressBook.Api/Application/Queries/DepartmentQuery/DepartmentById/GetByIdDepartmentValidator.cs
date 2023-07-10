using FluentValidation;

namespace AddressBook.Api.Application.Queries.DepartmentQuery.DepartmentById
{
    public class GetByIdDepartmentValidator : AbstractValidator<GetByIdDepartmentQuery>
    {
        public GetByIdDepartmentValidator()
        {
            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("Name cannot be null")
                .GreaterThan(0).WithMessage("ParentId should be greater than 0");
        }
    }
}
