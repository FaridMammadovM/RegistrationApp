using FluentValidation;

namespace AddressBook.Api.Application.Commands.CarCommand.Update
{
    public class CarUpdateCommandValidator : AbstractValidator<CarUpdateCommand>
    {
        public CarUpdateCommandValidator()
        {
            RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("Id cannot be null")
                            .GreaterThan(0).WithMessage("Id should be greater than 0");
            RuleFor(v => v.Brand).NotNull().NotEmpty().WithMessage("Brand cannot be null");
            RuleFor(v => v.Model).NotNull().NotEmpty().WithMessage("Model cannot be null");
            RuleFor(v => v.Number).NotNull().NotEmpty().WithMessage("Number cannot be null");
            RuleFor(v => v.Seat).NotNull().NotEmpty().WithMessage("Seat cannot be null")
                .GreaterThan(0).WithMessage("Seat should be greater than 0");
            RuleFor(v => v.Kind).NotNull().NotEmpty().WithMessage("Kind cannot be null");
            RuleFor(v => v.Color).NotNull().NotEmpty().WithMessage("Color cannot be null");
            RuleFor(v => v.Year).NotNull().NotEmpty().WithMessage("Year cannot be null")
                                    .Must(year => year.Length == 4).WithMessage("Year 4 basamaklı olmalıdır.");


        }
    }
}