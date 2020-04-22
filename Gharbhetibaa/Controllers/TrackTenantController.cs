using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Images;
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
    public class TrackTenantController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: TrackTenant
        public ActionResult Index(int TenantID)
        {
            var tenantObj = db.Tenants.Where(x => x.TenantID == TenantID).FirstOrDefault();
            return PartialView(tenantObj);
        }

        public ActionResult AddTenant(int PropertyID)
        {
            ViewBag.PropertyID = PropertyID;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddTenant(string ContractPic, int PropertyID, Tenant tenantObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                using (db)
                {

                    UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

                    UserTenant usrTenant = new UserTenant
                    {
                        UserID = usr.UserID,
                        PropertyID = PropertyID,
                        TenantID = tenantObj.TenantID
                    };
                    db.Tenants.Add(tenantObj);
                    db.UserTenants.Add(usrTenant);

                    //Image
                    var pictureIDs = ContractPic.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                    tenantObj.PictureContractDocs = new List<PictureContractDoc>();
                    tenantObj.PictureContractDocs.AddRange(pictureIDs.Select(x => new PictureContractDoc() { ImageID = x }).ToList());
                    //End of Image

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

        
        public ActionResult EditTenant(int? TenantID)
        {
            if (TenantID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tenant tenantObj = db.Tenants.Find(TenantID);
            if (tenantObj == null)
            {
                return HttpNotFound();
            }

            return PartialView(tenantObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditTenant(string ContractPic, int? TenantID, Tenant tenantObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                //Image
                var pictureIDs = ContractPic.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                tenantObj.PictureContractDocs = new List<PictureContractDoc>();
                tenantObj.PictureContractDocs.AddRange(pictureIDs.Select(x => new PictureContractDoc() { TenantID = tenantObj.TenantID, ImageID = x }).ToList());
                //End of Image

                var existingItems = db.Tenants.Find(TenantID);

                db.PictureContractDocs.RemoveRange(existingItems.PictureContractDocs);

                db.Entry(existingItems).CurrentValues.SetValues(tenantObj);

                db.PictureContractDocs.AddRange(tenantObj.PictureContractDocs);



                //db.Entry(tenantObj).State = System.Data.Entity.EntityState.Modified;

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
        public ActionResult DeleteTenant(int? PropertyID, int? TenantID, Tenant tenantObj)
        {
            //remove picture

            //Remove tenant
            tenantObj = db.Tenants.Find(TenantID);
            db.Tenants.Remove(tenantObj);

            //Remove Rent
            UserAccount userObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            var rentObj = db.UserRentDetails.Where(x => x.UserID == userObj.UserID).Where(x => x.PropertyID == PropertyID).Where(x => x.TenantID == TenantID).Select(x => x.RentDetail);
            foreach (var i in rentObj)
            {
                db.RentDetails.Remove(db.RentDetails.Find(i.RentID));
            }

            db.SaveChanges();

            return RedirectToAction("MyTenants", "PropertyManagement");
        }
    }
}