using SQLite;

namespace MauiForeignKey.Models
{
    [Table("departments")]
    public class Department : BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
