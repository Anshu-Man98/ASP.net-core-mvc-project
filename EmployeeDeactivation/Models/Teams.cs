using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDeactivation.Models
{
    public class Teams
    {
      

        public string TeamName { get; set; }
       
        [Key]
        public string SponsorGID { get; set; }
       
       
    }

    
}
