using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.PropertyManagement;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: Expense
        public ActionResult Index(int ExpenseID)
        {
            var expenseObj = db.UserExpenses.Where(x => x.ExpenseID == ExpenseID).Select(x => x.Expense).FirstOrDefault();
            return PartialView(expenseObj);
        }


        public ActionResult AddExpense(int PropertyID)
        {
            ViewBag.PropertyID = PropertyID;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddExpense(int PropertyID, Expense expenseObj)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

                    UserExpense usrExpense = new UserExpense
                    {
                        UserID = usr.UserID,
                        PropertyID = PropertyID,
                        ExpenseID = expenseObj.ExpenseID
                    };
                    db.Expenses.Add(expenseObj);
                    db.UserExpenses.Add(usrExpense);

                    db.SaveChanges();
                    result.Data = new { Success = true };

                }
            }
            else
            {
                result.Data = new { Success = false, Error = "Unable to save. Please enter valid values." };
            }
            return result;

        }


        public ActionResult EditExpense(int? ExpenseID)
        {
            if (ExpenseID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Expense expenseObj = db.Expenses.Find(ExpenseID);
            if (expenseObj == null)
            {
                return HttpNotFound();
            }

            return PartialView(expenseObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditExpense(int? ExpenseID, Expense expenseObj)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                db.Entry(expenseObj).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false, Error = "Unable to save. Please enter valid values." };
            }
            return result;
        }

        [HttpPost]
        public ActionResult DeleteExpense(int? ExpenseID, Expense expenseObj)
        {
            //Remove expense
            expenseObj = db.Expenses.Find(ExpenseID);
            db.Expenses.Remove(expenseObj);

            db.SaveChanges();

            return RedirectToAction("MyExpenses", "PropertyManagement");
        }


    }
}