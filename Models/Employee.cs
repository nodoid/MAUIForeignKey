using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MauiForeignKey.Models
{
    [Table("employees")]
    public class Employee 
    {
        public string Name { get; set; }
        [ForeignKey(typeof(Department))]
        public int DepartmentId { get; set; }
    }
}