using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryMSMVC.Models;

namespace LibraryMSMVC.Controllers
{

    public class AdminTransactionController : Controller
    {
        private LibraryMVCEntities adminTransactiondb = new LibraryMVCEntities();


        // Returns admin request view, here admin can accept and reject the book requests
        public ActionResult Requests()
        {
            return View(adminTransactiondb.tblTransactions.ToList());
        }
        // Returns all book requests in json format.
        public ActionResult GetAllRequests()
        {
            var transactionList = adminTransactiondb.tblTransactions.Where(r => r.TranStatus == "Requested").ToList();
            return Json(new { data = transactionList }, JsonRequestBehavior.AllowGet);
        }
        // Accepts the book request.
        public ActionResult AcceptRequest(int? tranId)
        {
         
            if (tranId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTransaction transaction = adminTransactiondb.tblTransactions.FirstOrDefault(t => t.TranId == tranId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            transaction.TranStatus = "Accepted";
            transaction.TranDate = DateTime.Now.ToShortDateString();
            adminTransactiondb.SaveChanges();
            return View("Requests");
            

        }
        // Reject the book request. 
        public ActionResult RejectRequest(int? tranId)
        {
            
            if (tranId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTransaction transaction = adminTransactiondb.tblTransactions.FirstOrDefault(t => t.TranId == tranId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            transaction.TranStatus = "Rejected";
            transaction.TranDate = DateTime.Now.ToShortDateString();
            tblBook book = adminTransactiondb.tblBooks.FirstOrDefault(b => b.BookId == transaction.BookId);
            book.BookCopies = book.BookCopies + 1;
            adminTransactiondb.SaveChanges();
            adminTransactiondb.SaveChanges();
            return View("Requests");
            
        }
        // Returns admin accepted view, here admin can view the accepted books.
        public ActionResult Accepted()
        {
            return View(adminTransactiondb.tblTransactions.ToList());
        }
        // Returns all accepted books in json format.
        public ActionResult GetAllAccepted()
        {
            var transactionList = adminTransactiondb.tblTransactions.Where(r => r.TranStatus == "Accepted").ToList();
            return Json(new { data = transactionList }, JsonRequestBehavior.AllowGet);
        }
        // Returns admin return view, here admin can accept book return requests.
        public ActionResult Return()
        {
            return View(adminTransactiondb.tblTransactions.ToList());
        }
        // Returns all return books in json format.
        public ActionResult GetAllReturn()
        {
            var transactionList = adminTransactiondb.tblTransactions.Where(r => r.TranStatus == "Returned").ToList();
            return Json(new { data = transactionList }, JsonRequestBehavior.AllowGet);
        }
        // Accepts the book return request.
        public ActionResult AcceptReturn(int? tranId)
        {

            
            if (tranId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblTransaction transaction = adminTransactiondb.tblTransactions.FirstOrDefault(t => t.TranId == tranId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            tblBook book = adminTransactiondb.tblBooks.FirstOrDefault(b => b.BookId == transaction.BookId);
            book.BookCopies = book.BookCopies + 1;
            adminTransactiondb.SaveChanges();
            adminTransactiondb.tblTransactions.Remove(transaction);
            adminTransactiondb.SaveChanges();
            return View("Return");
        }
        // Returns admin home view.
        public ActionResult AdminHome()
        {
            return View();
        }
        // Returns admin about view.
        public ActionResult AdminAbout()
        {
            return View();
        }
        // Returns admin contact view.
        public ActionResult AdminContact()
        {
            return View();
        }
    }
}