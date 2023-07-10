namespace AddressBook.Domain.Dtos.Employee
{
    public sealed class EmployeeIdDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int PositionId { get; set; }
        public string Email { get; set; }
        public string InternalTelephone { get; set; }
        public string MobilPhone { get; set; }
        public string Floor { get; set; }
        public string RoomNumber { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int? UserRole { get; set; }
        public string UserName { get; set; }
        public int OrderNo { get; set; }
        public int Gender { get; set; }
    }
}
