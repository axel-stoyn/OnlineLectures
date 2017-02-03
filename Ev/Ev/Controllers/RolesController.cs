using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ev.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ev.Controllers
{
    public class RolesController : Controller
    {
        private OurDbContext db = new OurDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Create (FormCollection collection)
        //{
        //    try
        //    {
        //        db.Roles.Add(new IdentityRole()
        //        {
        //           Name = collection[]
        //        });
        //        db.SaveChanges();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        }
    }
