namespace AddressBook.Domain.Dtos.Employee
{
    public sealed class EmployeeGetAllReturnDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Position_name { get; set; }
        public string Email { get; set; }
        public string Internal_telephone { get; set; }
        public string Mobile_phone { get; set; }
        public string Floor { get; set; }
        public string Room_number { get; set; }
        public string org_structure_name { get; set; }
        public int order_no { get; set; }
    }
}
