using LibraryMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMSMVC.Controllers
{
    public class RegistrationController : Controller
    {
        private LibraryMVCEntities db = new LibraryMVCEntities();
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Registration obj)

        {
            if (ModelState.IsValid)
            {
                
                db.Registrations.Add(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
      
    }
}