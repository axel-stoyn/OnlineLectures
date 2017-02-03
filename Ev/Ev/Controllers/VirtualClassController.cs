using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ev.Models;
using System.Net;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace Ev.Controllers
{
    public class VirtualClassController : Controller
    {
        private OurDbContext db = new OurDbContext();

        public ActionResult Index(int? SelectedTraining)
        {
            var trainingss = db.Training.OrderBy(q => q.NameTraining).ToList();
            ViewBag.SelectedTraining = new SelectList(trainingss, "TrainingID", "NameTraining", SelectedTraining);
            int trainingID = SelectedTraining.GetValueOrDefault();

            IQueryable<VirtualClass> virtualClass = db.VirtualClass
                .Where(c => !SelectedTraining.HasValue || c.TrainingID == trainingID)
                .OrderBy(d => d.VirtualClassID)
                .Include(d => d.Training);
            var sql = virtualClass.ToString();
            return View(virtualClass.ToList());
        }

        public ActionResult Create()
        {
            TrainingDropDownList();
            return View();
        }

        // POST: VirtualClass/Create
        [HttpPost]
        //[Authorize(Roles = "trainer")]
        public ActionResult Create(VirtualClass virtualClass)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.VirtualClass.Add(virtualClass);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again or inform administration");
            }
            ModelState.Clear();
            ViewBag.Message = virtualClass.NameVirtualClass;
            TrainingDropDownList(virtualClass.TrainingID);
            return View(virtualClass);
        }

        private void TrainingDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in db.Training
                                   orderby d.NameTraining
                                   select d;
            ViewBag.TrainingID = new SelectList(departmentsQuery, "TrainingID", "NameTraining", selectedDepartment);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var virtualClass = db.VirtualClass.Find(id);
            if (virtualClass == null)
            {
                return HttpNotFound();
            }
            return View(virtualClass);
        }

        [HttpGet]
        //[Authorize(Roles = "trainer")]
        public ActionResult Edit(int? id)
        {
            VirtualClass virtualClass = db.VirtualClass.Find(id);
            TrainingDropDownList(virtualClass.TrainingID);
            return View(virtualClass);
        }

        [HttpPost, ActionName("Edit")]
        //[Authorize(Roles = "trainer")]
        public ActionResult EditPost(int? id)
        {
            var virtualClassToUpdate = db.VirtualClass.Find(id);
            if (TryUpdateModel(virtualClassToUpdate, "",
               new string[] { "NameVirtualClass", "TrainingID" }))
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            TrainingDropDownList(virtualClassToUpdate.TrainingID);
            return View(virtualClassToUpdate);
        }

        [HttpGet]
        //[Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var virtualClass = db.VirtualClass.Find(id);
            if (virtualClass == null)
            {
                return HttpNotFound();
            }
            return View(virtualClass);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var virtualClassToDelite = db.VirtualClass.Find(id);
            db.VirtualClass.Remove(virtualClassToDelite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}