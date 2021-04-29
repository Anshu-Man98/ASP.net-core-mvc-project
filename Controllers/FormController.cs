using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeactivationForm.Models;
using DeactivationForm.Context;

namespace DeactivationForm.Controllers
{
    public class FormController : Controller
    {
        private userContext db = new userContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "Fname,Lname,email,gid,date")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(user);
        }
    } 
}