using DapperExtension;

namespace AddressBook.Domain.Models
{
    [Table(TableName = "\"AB_ORG\".position", KeyName = "id", KeyNameInClass = "Id", IsIdentity = true)]
    public class Position
    {
        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "position_name")]
        public string Name { get; set; }
    }
}
