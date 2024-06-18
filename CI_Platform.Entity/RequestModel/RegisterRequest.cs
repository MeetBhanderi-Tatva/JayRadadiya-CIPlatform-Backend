using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity.RequestModel
{
    public class RegisterRequest
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter a 10 digits number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=[^A-Z]*[A-Z])(?=[^a-z]*[a-z])(?=\\D*\\d).{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password",ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
