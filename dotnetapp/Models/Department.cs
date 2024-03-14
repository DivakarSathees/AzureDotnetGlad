namespace dotnetapp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Navigation property for Employees in this Department public 
        ICollection<Employee> Employees { get; set; }
    }
}
