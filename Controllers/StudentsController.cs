using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExperienceITBootcamp.Models;
using Microsoft.Ajax.Utilities;

namespace ExperienceITBootcamp.Controllers
{
    public class StudentsController : Controller
    {
        //Creating object from Context class,
        //using this object outside the method will access db for all action methods,so dont need to specify in each method
        private BootCampContext db = new BootCampContext();//to access and save data
        public ActionResult List()
        {
            var studentsList = db.Students.ToList(); //connecting to db getting students list
            return View(studentsList);
        }

        public ActionResult Create()
        {
            var coursesList = db.Courses.ToList();//1)to create dropdown,connect to db.
            var studentFormViewModel = new StudentFormViewModel//2)create object
            {
                courses = coursesList
            };
            return View(studentFormViewModel); //creating new form
        }
        
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentFormViewModel studentFormViewModel)//inserting form values in Dbase
        {
            //The student should be more than 18 years old.
            if (ModelState.IsValidField("students.DateOfBirth"))
            {
                int age = determineAge(studentFormViewModel.students.DateOfBirth);
                if (age <= 18)
                    ModelState.AddModelError("students.DateOfBirth", "Student has to be 18 years old and above");
            }
            if (ModelState.IsValid)//checking required fields is true
            {
                db.Students.Add(studentFormViewModel.students);
                db.SaveChanges();
                return RedirectToAction("List");//if false returning to form pg
            }

            studentFormViewModel.courses = db.Courses.ToList();
            return View(studentFormViewModel);
        }

        private int determineAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;
            return age;
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            //below code,for dropdown to show dropdown in edit page

            var studentsViewModel = new StudentFormViewModel()
            {
                students = student,
                courses = db.Courses.ToList()
            };
            return View(studentsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentFormViewModel studentFormViewModel)
        {
            if (ModelState.IsValidField("students.DateOfBirth"))
            {
                int age = determineAge(studentFormViewModel.students.DateOfBirth);
                if (age <= 18)
                    ModelState.AddModelError("students.DateOfBirth", "Student has to be 18 years old and above");
            }
            if (ModelState.IsValid)
            {
                db.Entry(studentFormViewModel.students).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            var studentsViewModel = new StudentFormViewModel()//created during dropdown
            {
                students = studentFormViewModel.students,
                courses = db.Courses.ToList()
            };
            return View(studentsViewModel);
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Students student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("List");
        }


    }
}


   







    
