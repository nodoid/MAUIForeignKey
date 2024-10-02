using SQLite;

namespace MauiForeignKey.Models
{
    public class BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
