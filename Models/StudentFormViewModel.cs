using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExperienceITBootcamp.Models
{
    public class StudentFormViewModel 
        // creating this class for binding courses and students class,and also
        // to create course drop down in student controller Action create and edit .cshtml
    {
        //creating userdefined Datatype followed by variable
        public Students students { get; set; }
        public IEnumerable<Courses> courses { get; set; }
    }
}