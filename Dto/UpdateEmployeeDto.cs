using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Api.DTOs
{
    public class UpdateEmployeeDto
    {
        //Name is required feild,maximu length of 100
        
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;


        //email is required feild,mximu length of 100
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;


        //department is required feild,maximum length of 50
        [Required(ErrorMessage = "Department is required")]
        [MaxLength(50, ErrorMessage = "Department cannot exceed 50 characters")]
        public string Department { get; set; } = string.Empty;


        //salary 10 digits with 2 decial places.
        [Required(ErrorMessage = "Salary is required")]
        [Range(0, 999999.99, ErrorMessage = "Salary must be between 0 and 999999.99")]
        public decimal Salary { get; set; }
    }
}