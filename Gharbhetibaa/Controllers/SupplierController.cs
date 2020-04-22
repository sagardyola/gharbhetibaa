using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.PropertyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: Supplier
        public ActionResult Index(int SupplierID)
        {
            var supplierObj = db.UserSuppliers.Where(x => x.SupplierID == SupplierID).Select(x => x.Supplier).FirstOrDefault();
            return PartialView(supplierObj);
        }

        public ActionResult AddSupplier()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddSupplier(Supplier supplierObj)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

                    db.Suppliers.Add(supplierObj);
                    UserSupplier usrSupp = new UserSupplier
                    {
                        UserID = usr.UserID,
                        SupplierID = supplierObj.SupplierID
                    };

                    db.UserSuppliers.Add(usrSupp);
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

        public ActionResult EditSupplier(int? SupplierID)
        {
            if (SupplierID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Supplier supplierObj = db.Suppliers.Find(SupplierID);
            if (supplierObj == null)
            {
                return HttpNotFound();
            }

            return PartialView(supplierObj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditSupplier(int? SupplierID, Supplier supplierObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                db.Entry(supplierObj).State = System.Data.Entity.EntityState.Modified;

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
        public ActionResult DeleteSupplier(int? SupplierID, Supplier supplierObj)
        {
            supplierObj = db.Suppliers.Find(SupplierID);

            //Remove Item
            db.Suppliers.Remove(supplierObj);

            db.SaveChanges();

            return RedirectToAction("MySuppliers", "PropertyManagement");
        }

    }
}