using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExperienceITBootcamp.Models
{
    public class BootCampContext : DbContext
    {
        //create ctor and set connection string
        public BootCampContext() :base("name=BootCampContext")
        {
            
        }
        //creating fields to fetch data from database
        public DbSet<Students> Students { get; set; }
        public DbSet<Courses> Courses { get; set; }
    }
}