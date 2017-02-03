using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ev.Models;
using System.Net;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace Ev.Controllers
{
    public class TrainingController : Controller
    {
        private OurDbContext db = new OurDbContext();

        // GET: Training
        public ActionResult Index()
        {
            return View(db.Training.ToList());
        }

        [Authorize(Roles = "admin")]
        [Authorize(Roles = "trainer")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Training/Create
        [HttpPost]
        //[Authorize(Roles = "trainer")]
        public ActionResult Create(Training training)
        {
            if (ModelState.IsValid)
            {
                db.Training.Add(training);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            ViewBag.Message = training.NameTraining + " " + training.DateStartOfTraining;
            return View();
        }

        [Authorize(Roles = "admin")]
        [Authorize(Roles = "trainer")]
        [HttpGet]
        //[Authorize(Roles = "trainer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // POST: VirtualTraining/Edit
        [HttpPost, ActionName("Edit")]
        //[Authorize(Roles = "trainer")]
        public ActionResult EditPost(int? id)
        {
            var virtualTraining = db.Training.Find(id);
            if (TryUpdateModel(virtualTraining, "",
               new string[] { "NameTraining", "DateStartOfTraining" }))
            {
               db.SaveChanges();
               return RedirectToAction("Index");
            }
            return View(virtualTraining);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var training = db.Training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }

            //Training training = db.Training.Find(id);
            return View(training);
        }

        [Authorize(Roles = "admin")]
        [Authorize(Roles = "trainer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Training.Find(id));
        }

        // POST: VirtualTraining/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var virtualTrainingToDelite = db.Training.Find(id);
                db.Training.Remove(virtualTrainingToDelite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to delete or inform to administrator");
                return View(db.Training.ToList());
            }
        }
    }
}