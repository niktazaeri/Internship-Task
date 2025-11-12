using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Task.Domain.Entities
{
    public class Product
    {
        [Key]
        [Column(TypeName ="int")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Filling this field is required."), Column(TypeName ="nvarchar(50)") , StringLength(50,ErrorMessage ="Maximum length for name is 50 charachters.")]
        public string Name { get; set; }
        [Required , Column(TypeName ="datetime2(3)")]
        public DateTime ProductDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Filling this field is required."), Phone(ErrorMessage ="Invalid phone number!")]
        [Column(TypeName ="nvarchar(15)")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number!")]
        public string ManufacturePhone { get; set; }
        [Required(ErrorMessage = "Filling this field is required."), EmailAddress(ErrorMessage ="Invalid email.") , Column(TypeName ="varchar(50)"), StringLength(50, ErrorMessage = "Maximum length for email is 50 charachters.")]
        public string ManufactureEmail { get; set; }
        [Required(ErrorMessage ="Filling this field is required.")]
        public bool IsAvailable { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
