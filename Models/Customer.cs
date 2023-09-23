using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myADOProject.Models
{
    public class Customer
    {
        [Key]
        public int cid { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage ="**Required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "**Required")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
        [Required(ErrorMessage = "**Required")]

        public DateTime DateofBirth { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "**Required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter the Valid User Email!")]

        public string Email { get; set; }

        [Required(ErrorMessage = "**Required")]
        public string Salary { get; set; }

        [NotMapped]
        public string Fullname { 
            get { return FirstName + " " + LastName; }
        }
    }
}
