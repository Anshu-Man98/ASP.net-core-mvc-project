using System;
using EmployeeDeactivation.Models;
using System.ComponentModel.DataAnnotations;


namespace EmployeeDeactivation.Models
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (DateTime)value > DateTime.Now;
        }
    }
}