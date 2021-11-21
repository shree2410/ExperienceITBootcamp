using ExperienceITBootcamp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExperienceITBootcamp.Controllers
{
    public class CoursesController : Controller
    {
        //Creating object for Context, using this object we will access database for all following methods
        private BootCampContext db = new BootCampContext();
        public ActionResult List()
        {
            var coursesList = db.Courses.ToList(); //connecting to db getting courses list
            return View(coursesList);
        }

        public ActionResult Create()
        {
            return View(); //render empty form
        }
        [HttpPost]//this tells which actn mtd to call when user submit the form

        [ValidateAntiForgeryToken]
        public ActionResult Create(Courses course)//inserting form values in Dbase
        {
            if (ModelState.IsValid)//checking required fields is true
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("List");//if false returning to form pg
            }
            return View(course);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses vCourses = db.Courses.Find(id);
            if (vCourses == null)
            {
                return HttpNotFound();
            }

            return View(vCourses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(courses);
        }
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
               Courses courses = db.Courses.Find(id);
                db.Courses.Remove(courses);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new {id = id, saveChangesError = true});
            }
            return RedirectToAction("List");
        }


    }
}
    