using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EmployeeDeactivation.Controllers;

namespace EmployeeDeactivation.Models
{
    public class Employee
    {
        [Required]
        [StringLength(30, ErrorMessage = "FirstName sholud not excede be more than 30 letters")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "FirstName sholud not contain Special characters")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "LastName sholud not excede be more than 30 letters")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "LastName sholud not contain Special characters")]
        public string Lastname { get; set; }

        [Required(ErrorMessage ="email address is Required")]
        [MaxLength(50)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "please enter valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Key]
        [Required(ErrorMessage = "GID is Required")]
        public string gId { get; set; }

        [FutureDate(ErrorMessage ="Date should be in the future")]
        public DateTime date { get; set; }

       

    }
   

}
