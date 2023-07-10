using DapperExtension;

namespace AddressBook.Domain.Models
{
    [Table(TableName = "\"AB_CAR\".car", KeyName = "id", KeyNameInClass = "Id", IsIdentity = true)]
    public class Car
    {
        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "model")]
        public string Model { get; set; }

        [Column(Name = "brand")]
        public string Brand { get; set; }

        [Column(Name = "number")]
        public string Number { get; set; }

        [Column(Name = "seat")]
        public int Seat { get; set; }

        [Column(Name = "kind")]
        public string Kind { get; set; }

        [Column(Name = "color")]

        public string Color { get; set; }

        [Column(Name = "year")]

        public string Year { get; set; }

        [Column(Name = "insert_user")]
        public string InsertUser { get; set; }

        [Column(Name = "update_user")]
        public string UpdateUser { get; set; }


        [Column(Name = "insert_date")]
        public DateTime? InsertDate { get; set; }

        [Column(Name = "update_date")]
        public DateTime? UpdateDate { get; set; }
        [Column(Name = "is_deleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
