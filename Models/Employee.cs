namespace Promtech_Machine_Test.Models
{
    //Creating Employee Model

    public class Employee
    {
        
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime CreatedOn { get; set; }



    }
}
