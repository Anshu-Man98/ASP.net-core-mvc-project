using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDeactivation.Models
{
    public class EmployeeDeactivationContext : DbContext
    {
        public EmployeeDeactivationContext (DbContextOptions<EmployeeDeactivationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EmployeeDeactivation.Models.Employee> Employee { get; set; }
    }
}
