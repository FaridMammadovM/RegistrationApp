using DapperExtension;

namespace AddressBook.Domain.Models.Employee
{
    [Table(TableName = "\"AB_ORG\".employee", KeyName = "id", KeyNameInClass = "Id", IsIdentity = true)]
    public class Employee : BaseEntity
    {
        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "surname")]
        public string Surname { get; set; }

        [Column(Name = "patronymic")]
        public string Patronymic { get; set; }

        [Column(Name = "position_id")]
        public int PositionId { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "internal_telephone")]
        public int InternalTelephone { get; set; }

        [Column(Name = "mobile_phone")]
        public string MobilPhone { get; set; }

        [Column(Name = "floor")]
        public string Floor { get; set; }

        [Column(Name = "room_number")]
        public string? RoomNumber { get; set; }

        [Column(Name = "password")]
        public string? Password { get; set; }

        [Column(Name = "structure_id")]
        public int DepartmentId { get; set; }

        [Column(Name = "user_role")]
        public string UserRole { get; set; }

        [Column(Name = "user_name")]
        public string UserName { get; set; }

        [Column(Name = "order_no")]
        public int OrderNo { get; set; }

        [Column(Name = "gender")]
        public string Gender { get; set; }
    }


}
