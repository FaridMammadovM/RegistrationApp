using FluentValidation;

namespace AddressBook.Api.Application.Queries.OrderQuery.GetById
{
    public class OrderGetByIdValidator : AbstractValidator<OrderGetByIdQuery>
    {
        public OrderGetByIdValidator()
        {
            RuleFor(v => v.Id).NotEqual(0).NotNull().NotEmpty().WithMessage("Id cannot be null");
        }
    }
}
