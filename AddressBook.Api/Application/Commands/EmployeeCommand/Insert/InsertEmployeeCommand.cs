using MediatR;

namespace AddressBook.Api.Application.Commands.EmployeeCommand.Insert
{
    public class InsertEmployeeCommand : IRequest<int?>
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public int InternalTelephone { get; set; }
        public string Email { get; set; }
        public string RoomNumber { get; set; }
        public string Floor { get; set; }
        public string MobilPhone { get; set; }
        public string? Password { get; set; }
        public string? RepeatPassword { get; set; }

        public int UserRole { get; set; }
        public string Username { get; set; }
        public int OrderNo { get; set; }
        public int Gender { get; set; }

    }
}
