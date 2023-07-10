using FluentValidation;

namespace AddressBook.Api.Application.Commands.OrderCommand.InsertRoom
{
    public class InsertOrderRoomCommandValidator : AbstractValidator<InsertOrderRoomCommand>
    {
        public InsertOrderRoomCommandValidator()
        {
            RuleFor(v => v.InsertBy).NotNull().NotEmpty().WithMessage("InsertBy cannot be null")
                .GreaterThan(0).WithMessage("EmployeeId should be greater than 0");
            RuleFor(v => v.MeetName).NotNull().NotEmpty().WithMessage("MeetName cannot be null");
            RuleFor(v => v.ParticipantCount).NotNull().NotEmpty().WithMessage("ParticipantCount cannot be null")
                .GreaterThan(0).WithMessage("ParticipantCount should be greater than 0");
            RuleFor(v => v.Time).NotNull().NotEmpty().WithMessage("Time cannot be null")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Time should be greater than or equal to today's date");
            RuleFor(v => v.RoomId).NotNull().NotEmpty().WithMessage("RoomId cannot be null")
                .GreaterThan(0).WithMessage("RoomId should be greater than 0");
            RuleFor(v => v.StartHours).NotNull().NotEmpty().WithMessage("StartHours cannot be null");
            RuleFor(v => v.EndHours).NotNull().NotEmpty().WithMessage("EndHours cannot be null");
            RuleFor(v => v)
                .Custom((model, context) =>
                {
                    if (model.StartHours > model.EndHours)
                    {
                        context.AddFailure("StartHours should be less than EndHours");
                    }
                });



        }
    }
}
