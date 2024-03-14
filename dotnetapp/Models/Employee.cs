namespace dotnetapp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        // Profile Image 
        public string ProfileImageFileName { get; set; }
        // ID Proof Document 
        public string IdProofDocumentFileName { get; set; }
    }
}
