using SQLite;

namespace MauiForeignKey.Models
{
    [Table("departments")]
    public class Department : BaseTable
    {
        [PrimaryKey]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
    }
}
