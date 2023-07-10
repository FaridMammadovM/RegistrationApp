namespace AddressBook.Domain.Dtos.Employee
{
    public sealed class EmployeeGetAllDto
    {
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public int? PositionId { get; set; }
        public string? Floor { get; set; }
        public string? RoomNumber { get; set; }
        public int? DepartmentId { get; set; }
        public string? KeyWord { get; set; }
    }
}
