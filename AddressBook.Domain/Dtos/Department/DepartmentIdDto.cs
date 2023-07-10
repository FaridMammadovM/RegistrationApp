namespace AddressBook.Domain.Dtos.Department
{
    public sealed class DepartmentIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
