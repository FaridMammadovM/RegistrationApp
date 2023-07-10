using FluentValidation;

namespace AddressBook.Api.Application.Commands.OrderCommand.Insert
{
    public class OrderInsertValidator : AbstractValidator<OrderInsertCommand>
    {
        public OrderInsertValidator()
        {
            RuleFor(v => v.EmployeeId).NotNull().NotEmpty().WithMessage("EmployeeId cannot be null")
                .GreaterThan(0).WithMessage("EmployeeId should be greater than 0");
            RuleFor(v => v.PassengerType).NotNull().NotEmpty().WithMessage("PassengerType cannot be null")
                .Must(x => x == 1 || x == 2).WithMessage("PassengerType must be 1 or 2.");
            When(v => v.PassengerType == 1, () =>
            {
                RuleFor(v => v.EmployeeUsingId)
                    .NotNull().NotEmpty().WithMessage("EmployeeUsingId cannot be null or empty.")
                    .GreaterThan(0).WithMessage("EmployeeId should be greater than 0");
            });

            When(v => v.PassengerType == 2, () =>
            {
                RuleFor(v => v.EmployeeUsingId)
                    .Null().WithMessage("EmployeeUsingId must be null.");
            });
            RuleFor(v => v.DepartureType).NotNull().NotEmpty().WithMessage("Name cannot be null")
                 .Must(x => x == 1 || x == 2).WithMessage("DepartureType must be 1 or 2.");

            When(v => v.DepartureType == 1, () =>
            {
                RuleFor(v => v.DepartureTimeDay).Null().WithMessage("DepartureTimeDay must be null.");
                RuleFor(v => v.DepartureTimeHours).NotNull().NotEmpty().WithMessage("DepartureTimeHours cannot be null");

                When(v => v.Direction == 2, () =>
                {
                    RuleFor(v => v.ReturnTimeHours).NotNull().NotEmpty().WithMessage("ReturnTimeHours cannot be null");
                });
                When(v => v.Direction == 1, () =>
                {
                    RuleFor(v => v.ReturnTimeHours).Null().WithMessage("ReturnTimeHours must be null");
                });

                RuleFor(v => v.ReturnTimeDay)
                    .Null().WithMessage("ReturnTimeDay must be null.");

                RuleFor(v => v.ReturnTimeHours).GreaterThan(v => v.DepartureTimeHours)
                    .WithMessage("DepartureTimeHours must be greaters than ReturnTimeHours.");

                RuleFor(v => v.DepartureTimeHours)
               .Must((model, value) =>
               {
                   if (value == null) return false; // DepartureTimeHours null olamaz

                   var selectedTime = (TimeSpan)value;

                   var now = DateTime.Now.AddHours(4); // Saat farkını hesaba kat
                   if (now.TimeOfDay < new TimeSpan(15, 0, 0))
                   {
                       // Şu an saat 15:00'ten önceyse, günün olduğu saatten sonraki herhangi bir saat seçilebilir
                       return selectedTime >= now.TimeOfDay;
                   }
                   else
                   {
                       // Şu an saat 15:00'ten sonra ise 15:00'den sonraki saatler ve 00:00-09:00 arası seçilebilir
                       var start = TimeSpan.Parse("15:00");
                       var end = TimeSpan.Parse("23:59:59");
                       var midnightStart = TimeSpan.Parse("00:00");
                       var midnightEnd = TimeSpan.Parse("09:59:59");

                       return (selectedTime >= start && selectedTime <= end) || (selectedTime >= midnightStart && selectedTime <= midnightEnd);
                   }
               })
               .WithMessage("Invalid DepartureTimeHours value.");


            });

            When(v => v.DepartureType == 2, () =>
            {
                RuleFor(v => v.DepartureTimeDay).NotNull().NotEmpty().WithMessage("DepartureTimeDay cannot be null");
                RuleFor(v => v.ReturnTimeDay).NotNull().NotEmpty().WithMessage("ReturnTimeDay cannot be null");
                RuleFor(v => v.DepartureTimeHours)
                    .NotNull().WithMessage("DepartureTimeHours cannot be null.");
                RuleFor(v => v.ReturnTimeHours)
                   .Null().WithMessage("ReturnTimeHours must be null.");
                RuleFor(v => v.ReturnTimeDay)
    .Must((model, returnTime) => returnTime >= model.DepartureTimeDay)
    .WithMessage("ReturnTimeDay must be greater than DepartureTimeDay.");
            });

            When(v => v.DepartureType == 2 && v.DepartureTimeDay == DateTime.UtcNow.Date, () =>
            {
                RuleFor(v => v.DepartureTimeHours)
                    .NotNull().WithMessage("DepartureTimeHours cannot be null.")
                    .Must(hour => hour >= TimeSpan.FromHours(DateTime.UtcNow.Hour)).WithMessage("DepartureTimeHours must be greater than or equal to current hour.");
            });

            RuleFor(v => v.Direction).NotNull().NotEmpty().WithMessage("Direction cannot be null")
                 .Must(x => x == 1 || x == 2).WithMessage("Direction must be 1 or 2.");
            RuleFor(v => v.PassengerCount).NotNull().NotEmpty().WithMessage("Address cannot be null")
                .GreaterThan(0).WithMessage("PassengerCount should be greater than 0");

            When(v => v.Luggage == 1, () =>
            {
                RuleFor(v => v.LuggageSize).NotNull().NotEmpty().WithMessage("LuggageSize cannot be null");
            });
            When(v => v.Luggage == 2, () =>
            {
                RuleFor(v => v.LuggageSize)
                    .Null().WithMessage("LuggageSize must be null.");
            });
            RuleFor(v => v.Address).NotNull().NotEmpty().WithMessage("Address cannot be null");
        }
    }
}
