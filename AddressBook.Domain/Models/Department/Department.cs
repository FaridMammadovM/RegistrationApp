using DapperExtension;

namespace AddressBook.Domain.Models.Department
{
    [Table(TableName = "\"AB_ORG\".org_structure", KeyName = "id", KeyNameInClass = "Id", IsIdentity = true)]
    public class Department : BaseEntity
    {
        [Column(Name = "structure_name")]
        public string Name { get; set; }

        [Column(Name = "parent_id")]
        public int? ParentId { get; set; }

        //Navigation property
        // [DapperIgnore]
        //public Department Parent { get; set; }
    }
}
