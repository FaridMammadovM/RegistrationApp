using DapperExtension;

namespace AddressBook.Domain.Models.Project
{
    [Table(TableName = "project.projects", KeyName = "id", KeyNameInClass = "Id", IsIdentity = true)]
    public class ProjectModel
    {
        [Column(Name = "id")]
        public long Id { get; set; }
        [Column(Name = "project_name")]
        public string? ProjectName { get; set; }
        [Column(Name = "project_desc")]
        public string? ProjectDesc { get; set; }
        [Column(Name = "insert_date")]
        public DateTime InsertDate { get; set; }
        [Column(Name = "inserted_by")]
        public int? InsertedBy { get; set; }
        [Column(Name = "updated_by")]
        public int? UpdateBy { get; set; }
        [Column(Name = "update_date")]
        public DateTime UpdateDate { get; set; }

    }

}
