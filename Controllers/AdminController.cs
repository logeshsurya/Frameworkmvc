using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryMSMVC.Models;
using System.Web.Mvc;

namespace LibraryMSMVC.Controllers
{
    public class AdminController : Controller
    {
        private LibraryMVCEntities adminDb = new LibraryMVCEntities();

        // Returns admin login view, here admin can login.
        [HttpGet]
        [HandleError]
        public ActionResult Login()
        {
            return View();
        }

        // Checks admin credentials, redirecting to admin section (index, tblBooks). 
        [HttpPost]
        [HandleError]
        public ActionResult Login(tblAdmin admin)
        {
            var adm = adminDb.tblAdmins.SingleOrDefault(a => a.AdminEmail == admin.AdminEmail && a.AdminPass == admin.AdminPass);
            if (adm != null)
            {
                int id = adm.AdminId;
                Session["adminId"] = adm.AdminId;
                return RedirectToAction("Index", "TblBooks", new { id = id });
            }
            else if (admin.AdminEmail == null && admin.AdminPass == null)
            {
                return View();
            }
            ViewBag.Message = "User name and password are not matching";
            return View();
        }
        
        // Admin logout, redirect to main. 
        [HandleError]
        public ActionResult Logout()
        {
            Session.Remove("adminId");
            return RedirectToAction("Home", "Main");
        }
    }
}