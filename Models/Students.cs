using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExperienceITBootcamp.Models
{
    public class Students
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CourseId { get; set; }
        public DateTime CourseEnrolledDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public CourseStatusEnum CourseStatus { get; set; }
        [Required]
        public string Grade { get; set; }


    }

    public enum CourseStatusEnum
    {
        Completed =1,
        InProgress=0
    }
}