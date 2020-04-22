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
    public class TrackRentDetailController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: TrackRentDetail
        public ActionResult Index(int PropertyID, int TenantID)
        {
            var model = new PropertyManagementVM();
            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {
                model.PropertyID = PropertyID;
                model.TenantID = TenantID;
                model.PropertyName = Convert.ToString(db.Properties.Where(x => x.PropertyID == PropertyID).Select(x => x.Title).FirstOrDefault());
                model.TenantName = Convert.ToString(db.Tenants.Where(x => x.TenantID == TenantID).Select(x => x.TenantName).FirstOrDefault());
                model.RentDetails = db.UserRentDetails.Where(e => e.UserID == usr.UserID).Where(e => e.PropertyID == PropertyID).Where(e => e.TenantID == TenantID).Select(e => e.RentDetail).ToList();
            }
            return View(model);
        }

        public ActionResult AddRentDetail(int PropertyID, int TenantID)
        {
            ViewBag.PropertyID = PropertyID;
            ViewBag.TenantID = TenantID;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddRentDetail(RentDetail rent, int PropertyID, int TenantID)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
                    if (rent.MonthlyRent == null) rent.MonthlyRent = 0;
                    if (rent.Electricity == null) rent.Electricity = 0;
                    if (rent.WaterSupply == null) rent.WaterSupply = 0;
                    if (rent.Miscellaneous == null) rent.Miscellaneous = 0;
                    if (rent.AmountPayed == null) rent.AmountPayed = 0;

                    UserRentDetail usrRent = new UserRentDetail
                    {
                        UserID = usr.UserID,
                        PropertyID = PropertyID,
                        TenantID = TenantID,
                        RentID = rent.RentID
                    };
                    db.RentDetails.Add(rent);
                    db.UserRentDetails.Add(usrRent);

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

        public ActionResult EditRentDetail(int? RentID)
        {
            if (RentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RentDetail rentObj = db.RentDetails.Find(RentID);
            if (rentObj == null)
            {
                return HttpNotFound();
            }
            return PartialView(rentObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditRentDetail(int? RentID, RentDetail rentObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                if (rentObj.MonthlyRent == null) rentObj.MonthlyRent = 0;
                if (rentObj.Electricity == null) rentObj.Electricity = 0;
                if (rentObj.WaterSupply == null) rentObj.WaterSupply = 0;
                if (rentObj.Miscellaneous == null) rentObj.Miscellaneous = 0;
                if (rentObj.AmountPayed == null) rentObj.AmountPayed = 0;

                db.Entry(rentObj).State = System.Data.Entity.EntityState.Modified;

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
        public ActionResult DeleteRentDetail(int? PropertyID, int? TenantID, int? RentID, RentDetail rentObj)
        {
            rentObj = db.RentDetails.Find(RentID);
            db.RentDetails.Remove(rentObj);

            //UserAccount userObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            //UserRentDetail usrRentObj = db.UserRentDetails.Where(x => x.UserID == userObj.UserID).Where(x => x.PropertyID == PropertyID).Where(x => x.TenantID == TenantID).Where(x => x.RentID == RentID).FirstOrDefault();
            //db.UserRentDetails.Remove(usrRentObj);

            db.SaveChanges();

            return RedirectToAction("Index", "TrackRentDetail", new { PropertyID = PropertyID, TenantID = TenantID });
        }
    }
}