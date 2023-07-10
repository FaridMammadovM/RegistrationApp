using DapperExtension;

namespace AddressBook.Domain.Models
{
    public class BaseEntity
    {
        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column(Name = "insert_date")]
        public DateTime? InsertDate { get; set; }

        [Column(Name = "update_date")]
        public DateTime? UpdateDate { get; set; }

        [Column(Name = "insert_user")]
        public string? InsertUser { get; set; }

        [Column(Name = "update_user")]
        public string? UpdateUser { get; set; }
    }
}
