using FluentValidation;

namespace AddressBook.Api.Application.Queries.OrderQuery.RoomGetById
{
    public class RoomOrderGetByIdQueryValidator : AbstractValidator<RoomOrderGetByIdQuery>
    {
        public RoomOrderGetByIdQueryValidator()
        {
            RuleFor(v => v.Id).NotEqual(0).NotNull().NotEmpty().WithMessage("Id cannot be null");
        }
    }
}