using FluentValidation;

namespace AddressBook.Api.Application.Queries.CarQuery.GetById
{
    public class CarGetByIdQueryValidator : AbstractValidator<CarGetByIdQuery>
    {
        public CarGetByIdQueryValidator()
        {
            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("Id cannot be null")
                            .GreaterThan(0).WithMessage("Id should be greater than 0");

        }
    }
}