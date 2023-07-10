namespace AddressBook.Domain.Dtos.Employee
{
    public sealed class EmployeeUpdateDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Position { get; set; }
        public string Email { get; set; }
        public string InternalTelephone { get; set; }
        public string MobilPhone { get; set; }
        public byte Floor { get; set; }
        public string RoomNumber { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public string UserRole { get; set; }
    }
}
