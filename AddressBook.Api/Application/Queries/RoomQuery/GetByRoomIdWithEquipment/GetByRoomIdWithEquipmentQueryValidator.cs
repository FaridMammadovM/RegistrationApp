using FluentValidation;

namespace AddressBook.Api.Application.Queries.RoomQuery.GetByRoomIdWithEquipment
{
    public class GetByRoomIdWithEquipmentQueryValidator : AbstractValidator<GetByRoomIdWithEquipmentQuery>
    {
        public GetByRoomIdWithEquipmentQueryValidator()
        {
            RuleFor(v => v.Id).NotEqual(0).NotNull().NotEmpty().WithMessage("Id cannot be null");
        }
    }
}