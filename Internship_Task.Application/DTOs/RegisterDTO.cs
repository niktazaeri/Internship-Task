using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Application.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Filling this field is required."), StringLength(50, ErrorMessage = "Maximum length for email is 50 charachters.")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First name must contain only alphabets.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Filling this field is required."), StringLength(50, ErrorMessage = "Maximum length for email is 50 charachters.")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Last name must contain only alphabets.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Filling this field is required.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Filling this field is required."), EmailAddress(ErrorMessage = "Invalid email!"), StringLength(50, ErrorMessage = "Maximum length for email is 50 charachters.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Filling this field is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Filling this field is required."),StringLength(int.MaxValue,MinimumLength =4,ErrorMessage ="Password must be at least 4 characters.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Filling this field is required."), Compare("Password",ErrorMessage ="Passwords Don't match.")]
        public string VerifyPassword { get; set; }
    }
}
