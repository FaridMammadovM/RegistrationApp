using FluentValidation;

namespace AddressBook.Api.Application.Commands.OrderCommand.ConfirmRoom
{
    public class RoomConfirmOrderCommandValidator : AbstractValidator<RoomConfirmOrderCommand>
    {
        public RoomConfirmOrderCommandValidator()
        {
            RuleFor(v => v.Status).NotNull().NotEmpty().WithMessage("Status cannot be null")
                .GreaterThan(0).WithMessage("DriverId should be greater than 0");
            When(v => v.Status == 1, () =>
            {
                RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("OrderId cannot be null")
               .GreaterThan(0).WithMessage("Order should be greater than 0");
            });
            When(v => v.Status == 2, () =>
            {
                RuleFor(v => v.Id).NotNull().NotEmpty().WithMessage("OrderId cannot be null")
               .GreaterThan(0).WithMessage("Order should be greater than 0");
                RuleFor(v => v.RejectReason).NotNull().NotEmpty().WithMessage("RejectionReason cannot be null");
            });




        }
    }
}
