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
    public class TrackPropertyController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: TrackProperty
        public ActionResult Index(int PropertyID)
        {
            var propertyObj = db.UserProperties.Where(x => x.PropertyID == PropertyID).Select(x => x.Property).FirstOrDefault();
            return PartialView(propertyObj);
        }

        public ActionResult AddProperty()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddProperty(Property propertyObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount usrObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

                    db.Properties.Add(propertyObj);
                    UserProperty usrPropObj = new UserProperty
                    {
                        UserID = usrObj.UserID,
                        PropertyID = propertyObj.PropertyID
                    };

                    db.UserProperties.Add(usrPropObj);
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

        public ActionResult EditProperty(int? PropertyID)
        {
            if (PropertyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Property propertyObj = db.Properties.Find(PropertyID);
            if (propertyObj == null)
            {
                return HttpNotFound();
            }
            return PartialView(propertyObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditProperty(int? PropertyID, Property propertyObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                db.Entry(propertyObj).State = System.Data.Entity.EntityState.Modified;
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
        public ActionResult DeleteProperty(int? PropertyID, Property propertyObj)
        {
            //remove picture

            //Remove Property
            propertyObj = db.Properties.Find(PropertyID);
            db.Properties.Remove(propertyObj);

            UserAccount userObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            ////Remove expense
            var expObj = db.UserExpenses.Where(x => x.UserID == userObj.UserID).Where(x => x.PropertyID == PropertyID).Select(x => x.Expense);
            foreach (var i in expObj)
            {
                db.Expenses.Remove(db.Expenses.Find(i.ExpenseID));
            }

            ////Remove tenant
            var tenObj = db.UserTenants.Where(x => x.UserID == userObj.UserID).Where(x => x.PropertyID == PropertyID).Select(x => x.Tenant);
            foreach (var i in tenObj)
            {
                db.Tenants.Remove(db.Tenants.Find(i.TenantID));
            }

            ////Remove Rent
            var rentObj = db.UserRentDetails.Where(x => x.UserID == userObj.UserID).Where(x => x.PropertyID == PropertyID).Select(x => x.RentDetail);
            foreach (var i in rentObj)
            {
                db.RentDetails.Remove(db.RentDetails.Find(i.RentID));
            }

            db.SaveChanges();
            return RedirectToAction("MyProperties", "PropertyManagement");
        }
    }
}