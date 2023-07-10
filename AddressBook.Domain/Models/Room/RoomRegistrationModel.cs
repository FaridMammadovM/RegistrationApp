using DapperExtension;


namespace AddressBook.Domain.Models.Room
{
    [Table(TableName = "\"AB_ROOM\".room_registration", KeyName = "id", KeyNameInClass = "Id", IsIdentity = true)]

    public class RoomRegistrationModel
    {
        [Column(Name = "id")]
        public int Id { get; set; }

        [Column(Name = "meeting_name")]
        public string MeetName { get; set; }

        [Column(Name = "participant_count")]
        public int ParticipantCount { get; set; }

        [Column(Name = "registration_time")]
        public DateTime Time { get; set; }

        [Column(Name = "begin_time")]
        public TimeSpan StartHours { get; set; }

        [Column(Name = "end_time")]
        public TimeSpan EndHours { get; set; }

        [Column(Name = "room_id")]
        public int RoomId { get; set; }

        [Column(Name = "note")]
        public string? Note { get; set; }

        [Column(Name = "status")]
        public string Status { get; set; }

        [Column(Name = "reject_reason")]
        public string? RejectReason { get; set; }

        [Column(Name = "inserted_by")]
        public int InsertBy { get; set; }

        [Column(Name = "inserted_date")]
        public DateTime? InsertedDate { get; set; }

        [Column(Name = "updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Column(Name = "updated_by")]
        public int UpdateBy { get; set; }

        [Column(Name = "isdeleted")]
        public bool IsDeleted { get; set; }
    }
}
