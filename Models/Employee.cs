using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MauiForeignKey.Models
{
    [SQLite.Table("employees")]
    public class Employee : BaseTable
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(typeof(Department))]
        public int DepartmentId { get; set; }
        
        // needed for employeewithdepartment class
        public Employee ()
        {
        }

        public Employee(EmployeeWithDepartment empDep)
        {
            Id = empDep.Id;
            Name = empDep.Name;
            DepartmentId = empDep.DepartmentId;
        }
    }
}