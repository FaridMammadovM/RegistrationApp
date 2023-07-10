using FluentValidation;

namespace AddressBook.Api.Application.Commands.OrderCommand.Confirm
{
    public class ConfirmOrderValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderValidator()
        {
            RuleFor(v => v.Status).NotNull().NotEmpty().WithMessage("Status cannot be null")
                .GreaterThan(0).WithMessage("DriverId should be greater than 0");
            When(v => v.Status == 1, () =>
            {
                RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("OrderId cannot be null")
               .GreaterThan(0).WithMessage("Order should be greater than 0");
                RuleFor(v => v.CarId).NotNull().NotEmpty().WithMessage("CarId cannot be null")
                   .GreaterThan(0).WithMessage("CarId should be greater than 0");
                RuleFor(v => v.DriverId).NotNull().NotEmpty().WithMessage("DriverId cannot be null")
                   .GreaterThan(0).WithMessage("DriverId should be greater than 0");
            });
            When(v => v.Status == 2, () =>
            {
                RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("OrderId cannot be null")
               .GreaterThan(0).WithMessage("Order should be greater than 0");
                RuleFor(v => v.RejectionReason).NotNull().NotEmpty().WithMessage("RejectionReason cannot be null");
                RuleFor(v => v.CarId).Null().WithMessage("CarId must be null.");
                RuleFor(v => v.DriverId).Null().WithMessage("DriverId must be null.");
            });

        }
    }
}
