namespace AddressBook.Domain.Dtos.Department
{
    public sealed class DepartmentUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
