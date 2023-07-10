using DapperExtension;


namespace AddressBook.Domain.Models.Room
{
    [Table(TableName = "\"AB_ROOM\".ROOM", KeyName = "id", KeyNameInClass = "Id", IsIdentity = true)]
    public class RoomModel
    {
        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "name")]
        public string Name { get; set; }

        [Column(Name = "capacity")]
        public int Capacity { get; set; }

        [Column(Name = "isdeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "inserted_date")]
        public DateTime? InsertDate { get; set; }

        [Column(Name = "updated_date")]
        public DateTime? UpdateDate { get; set; }

        [Column(Name = "inserted_by")]
        public string? InsertUser { get; set; }

        [Column(Name = "updated_by")]
        public string? UpdateUser { get; set; }
    }
}
