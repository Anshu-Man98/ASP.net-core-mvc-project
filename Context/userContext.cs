using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DeactivationForm.Models;

namespace DeactivationForm.Context
{
    public class userContext:DbContext
    {
        public DbSet<user> users { get; set; }
    }
}