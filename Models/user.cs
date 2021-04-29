using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeactivationForm.Models
{
    public class user

    {
        
        public string Fname { get; set; }
        [Required]
        [StringLength(20,ErrorMessage ="Name not be exceed")]
        public string Lname { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+",ErrorMessage = "please enter valid email")]
        public string Email { get; set; }
        [Key]
        public string gId { get; set; }
        [Required (ErrorMessage ="last working date is required")]
        public DateTime date { get; set; }
    }
}

