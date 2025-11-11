using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Domain.Entities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Filling this field is required."),Column(TypeName ="nvarchar(50)"), StringLength(50, ErrorMessage = "Maximum length for email is 50 charachters.")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First name must contain only alphabets.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Filling this field is required."), Column(TypeName = "nvarchar(50)"), StringLength(50, ErrorMessage = "Maximum length for email is 50 charachters.")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Last name must contain only alphabets.")]
        public string LastName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
