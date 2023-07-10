namespace AddressBook.Domain.Dtos.Department
{
    public sealed class DepartmentCreateDto
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
