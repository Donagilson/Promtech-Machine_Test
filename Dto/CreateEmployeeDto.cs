using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Api.DTOs
{
    public class CreateEmployeeDto
    {

        // Name is required
        // Maximum length  is 100 characters
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;


        // Email is mandatory
        // Validates email format ,maximum length is 100 
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;


        // Department is mandatory
        // Maximum length allowed is 50 characters
        [Required(ErrorMessage = "Department is required")]
        [MaxLength(50, ErrorMessage = "Department cannot exceed 50 characters")]
        public string Department { get; set; } = string.Empty;


        // Salary is mandatory
        // Accepts values only between 0 and 999999.99
        [Required(ErrorMessage = "Salary is required")]
        [Range(0, 999999.99, ErrorMessage = "Salary must be between 0 and 999999.99")]
        public decimal Salary { get; set; }
    }
}