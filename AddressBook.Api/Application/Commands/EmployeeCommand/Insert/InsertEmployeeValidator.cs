using FluentValidation;

namespace AddressBook.Api.Application.Commands.EmployeeCommand.Insert
{
    public class InsertEmployeeValidator : AbstractValidator<InsertEmployeeCommand>
    {
        public InsertEmployeeValidator()
        {
            RuleFor(v => v.Firstname).NotNull().NotEmpty().WithMessage("Firstname cannot be null");
            RuleFor(v => v.Surname).NotNull().NotEmpty().WithMessage("Surname cannot be null");
            RuleFor(v => v.Patronymic).NotNull().NotEmpty().WithMessage("Patronymic cannot be null");
            RuleFor(v => v.PositionId).NotNull().NotEmpty().WithMessage("PositionId cannot be null")
                                                .GreaterThan(0).WithMessage("PositionId should be greater than 0");
            RuleFor(v => v.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId cannot be null")
                                                .GreaterThan(0).WithMessage("DepartmentId should be greater than 0");
            RuleFor(v => v.InternalTelephone).NotNull().NotEmpty().WithMessage("InternalTelephone cannot be null")
                                                .GreaterThan(0).WithMessage("DepartmentId should be greater than 0");
            RuleFor(v => v.Email).NotNull().NotEmpty().WithMessage("Email cannot be null");
            RuleFor(v => v.Floor).NotNull().NotEmpty().WithMessage("Floor cannot be null");
            RuleFor(v => v.MobilPhone).NotNull().NotEmpty().WithMessage("MobilPhone cannot be null");
            RuleFor(v => v.RepeatPassword).Equal(v => v.Password).WithMessage("Repeat password must match password");
            RuleFor(v => v.UserRole).NotNull().NotEmpty().WithMessage("UserRole cannot be null");
            RuleFor(v => v.OrderNo).NotNull().NotEmpty().WithMessage("OrderNo cannot be null")
                                                .GreaterThan(0).WithMessage("OrderNo should be greater than 0");
            RuleFor(v => v.Gender).NotNull().NotEmpty().WithMessage("Gender cannot be null")
                .Must(x => x == 1 || x == 2).WithMessage("Gender must be 1 or 2.");
        }
    }
}