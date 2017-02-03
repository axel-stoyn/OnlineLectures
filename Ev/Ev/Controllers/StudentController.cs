using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ev.Models;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Data.Entity;
using PagedList;

namespace Ev.Controllers
{
    public class StudentController : Controller
    {
        private OurDbContext db = new OurDbContext();
        // GET: Student
        //[Authorize(Roles = "user")]
        public ActionResult Index(int? SelectedVirtualClass)
        {
            var virtualClass = db.VirtualClass.OrderBy(q => q.NameVirtualClass).ToList();
            ViewBag.SelectedVirtualClass = new SelectList(virtualClass, "Virtual ClassID", "Name Virtual Class", SelectedVirtualClass);
            int virtualClassID = SelectedVirtualClass.GetValueOrDefault();

            IQueryable<Student> student = db.Students
                .Where(c => !SelectedVirtualClass.HasValue || c.VirtualClassID == virtualClassID)
                .OrderBy(d => d.StudentID)
                .Include(d => d.virtualClass);
            var sql = student.ToString();

            return View(student.ToList());
            //return View();
        }


        //[Authorize(Roles = "user")]
        public ActionResult Create()
        {
            VirtualClassDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                //return RedirectToAction("Index");
                }
            ModelState.Clear();
            ViewBag.Message = student.FirstName + " " + student.LastName;
            VirtualClassDropDownList(student.VirtualClassID);
            return View(student);
        }

        private void VirtualClassDropDownList(object selectedVirtualClass = null)
        {
            var virtualClassQuery = from d in db.VirtualClass
                                   orderby d.NameVirtualClass
                                   select d;
            ViewBag.VirtualClassID = new SelectList(virtualClassQuery, "VirtualClassID", "NameVirtualClass", selectedVirtualClass);
        }

        //[Authorize(Roles = "user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.Students.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "FirstName", "LastName" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        //[Authorize(Roles = "user")]
        public ActionResult Delete(int? id, bool? saveChangesError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again or inform to administrator";
            }
            var student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }

            return View(students);
        }

    }
}