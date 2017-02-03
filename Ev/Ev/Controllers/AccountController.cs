using Ev.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ev.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.UserAccount.ToList());
            }
            //return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.UserAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.Firstname + " " + account.LastName + " successfully registered";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user, string returnUrl)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.UserAccount.Single(u => u.Username == user.Username && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserID"] = user.UserID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
        //[Authorize(Roles = "user")]
        public ActionResult VirtualClass()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return ViewBag.Message = "You must create an account.";
            }
        }

        //[Authorize(Roles = "superuser")]
        public ActionResult Training()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return ViewBag.Message = "You сan't create a training.";
            }
        }

        
    }
}