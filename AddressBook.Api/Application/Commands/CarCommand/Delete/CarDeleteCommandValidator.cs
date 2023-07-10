using FluentValidation;

namespace AddressBook.Api.Application.Commands.CarCommand.Delete
{
    public class CarDeleteCommandValidator : AbstractValidator<CarDeleteCommand>
    {
        public CarDeleteCommandValidator()
        {
            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("Id cannot be null")
                            .GreaterThan(0).WithMessage("Id should be greater than 0");

        }
    }
}