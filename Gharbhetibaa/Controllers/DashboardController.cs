using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();
        readonly int pageSize = 8;

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            DashboardVM model = new DashboardVM();
            int user = db.UserAccounts.Select(x => x.UserID).Count();
            int activeUser = db.UserAccounts.Where(x => x.Status == true).Select(x => x.UserID).Count();
            int verifiedUser = db.UserAccounts.Where(x => x.IsVerified == true).Select(x => x.UserID).Count();
            model = new DashboardVM()
            {
                CountUser = user,
                
                CountActive = activeUser,
                CountInactive = user - activeUser,

                CountVerified = verifiedUser,
                CountNonVerified = user - verifiedUser,

                CountItem = db.ItemDetails.Select(x => x.ItemID).Count(),
                CountSearching = db.SearchingFors.Select(x => x.SearchingForID).Count(),

                CountContacted = db.ContactUss.Select(x => x.ContactUsID).Count(),
                CountNewsletter = db.Newsletters.Select(x => x.NewsletterID).Count()
            };

            return PartialView(model);
        }

        public ActionResult Users(int? pageNo)
        {
            DashboardVM model = new DashboardVM();

            var userObj = db.UserAccounts.AsEnumerable();

            int count = userObj.Count();
            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            userObj = userObj.OrderByDescending(x => x.UserID).Skip(skipCount).Take(pageSize);

            model.Pager = new Pager(count, pageNo, pageSize);
            model.UserAccounts = userObj;

            return PartialView(model);
        }

        public ActionResult VerifyUsers(int? pageNo)
        {
            DashboardVM model = new DashboardVM();

            var userObj = db.UserAccounts.Where(x => x.IsVerified == false).AsEnumerable();

            int count = userObj.Count();
            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            userObj = userObj.OrderByDescending(x => x.UserID).Skip(skipCount).Take(pageSize);

            model.Pager = new Pager(count, pageNo, pageSize);
            model.UserAccounts = userObj;

            return PartialView(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult VerifyUser(int? UserID)
        {
            var userObj = db.UserAccounts.Find(UserID);

            userObj.IsVerified = true;

            db.Entry(userObj).CurrentValues.SetValues(userObj);
            db.SaveChanges();
            return RedirectToAction("VerifyUsers", "Dashboard");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ActiveDeactive(int? UserID)
        {
            var userObj = db.UserAccounts.Find(UserID);

            if (userObj.Status == true)
            {
                userObj.Status = false;
            } else
            {
                userObj.Status = true;
            }
            
            db.Entry(userObj).CurrentValues.SetValues(userObj);
            db.SaveChanges();
            return RedirectToAction("Users", "Dashboard");
        }

        public ActionResult AddRoles()
        {
            return PartialView();
        }

        public ActionResult Items(int? pageNo)
        {
            DashboardVM model = new DashboardVM();

            var itemObj = db.ItemDetails.AsEnumerable();

            int count = itemObj.Count();
            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            itemObj = itemObj.OrderByDescending(x => x.ItemID).Skip(skipCount).Take(pageSize);

            model.Pager = new Pager(count, pageNo, pageSize);
            model.ItemDetails = itemObj;

            return PartialView(model);
        }

        public ActionResult FeaturedItems(int? pageNo)
        {
            DashboardVM model = new DashboardVM();

            var itemObj = db.UserFeatures.Select(i => i.ItemDetail).AsEnumerable();

            int count = itemObj.Count();
            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            itemObj = itemObj.OrderByDescending(x => x.ItemID).Skip(skipCount).Take(pageSize);

            model.Pager = new Pager(count, pageNo, pageSize);
            model.ItemDetails = itemObj;

            return PartialView(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ItemFeature(int ItemID, UserFeature itemFeatureObj)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount listedByUsr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

                    bool obj = db.UserFeatures.Any(x => x.ItemID == ItemID);

                    if (obj == false)
                    {
                        itemFeatureObj.ItemID = ItemID;
                        itemFeatureObj.UserID = listedByUsr.UserID;
                        db.UserFeatures.Add(itemFeatureObj);
                    }
                    else
                    {
                        UserFeature a = db.UserFeatures.Where(x => x.ItemID == ItemID).FirstOrDefault();
                        db.UserFeatures.Remove(a);
                    }

                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("ItemDetails", "Item", new { ItemID = ItemID });
                }
            }
            return View(itemFeatureObj);
        }


        public ActionResult SearchingFor(int? pageNo)
        {
            DashboardVM model = new DashboardVM();

            var srchObj = db.SearchingFors.AsEnumerable();

            int count = srchObj.Count();
            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            srchObj = srchObj.OrderByDescending(x => x.SearchingForID).Skip(skipCount).Take(pageSize);

            model.Pager = new Pager(count, pageNo, pageSize);
            model.SearchingFors = srchObj;

            return PartialView(model);
        }

        public ActionResult Contacted(int? pageNo)
        {
            DashboardVM model = new DashboardVM();

            var contactedObj = db.ContactUss.AsEnumerable();

            int count = contactedObj.Count();
            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            contactedObj = contactedObj.OrderByDescending(x => x.ContactUsID).Skip(skipCount).Take(pageSize);

            model.Pager = new Pager(count, pageNo, pageSize);
            model.ContactUss = contactedObj;

            return PartialView(model);
        }

        public ActionResult ContactedDetails(int ContactUsID)
        {
            var contactedObj = db.ContactUss.Where(x => x.ContactUsID == ContactUsID).FirstOrDefault();
            return PartialView(contactedObj);
        }

        public ActionResult Newsletters(int? pageNo)
        {
            DashboardVM model = new DashboardVM();

            var newsletterObj = db.Newsletters.AsEnumerable();

            int count = newsletterObj.Count();
            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            newsletterObj = newsletterObj.OrderByDescending(x => x.NewsletterID).Skip(skipCount).Take(pageSize);

            model.Pager = new Pager(count, pageNo, pageSize);
            model.Newsletters = newsletterObj;

            return PartialView(model);
        }

        public ActionResult Feedbacks(int? pageNo)
        {
            DashboardVM model = new DashboardVM();

            var feedbackObj = db.Feedbacks.AsEnumerable();

            int count = feedbackObj.Count();
            pageNo = pageNo ?? 1;

            var skipCount = (pageNo.Value - 1) * pageSize;

            feedbackObj = feedbackObj.OrderByDescending(x => x.FeedbackID).Skip(skipCount).Take(pageSize);

            model.Pager = new Pager(count, pageNo, pageSize);
            model.Feedbacks = feedbackObj;

            return PartialView(model);
        }

        public ActionResult FeedbackDetails(int FeedbackID)
        {
            var feedbackObj = db.Feedbacks.Where(x => x.FeedbackID == FeedbackID).FirstOrDefault();
            return PartialView(feedbackObj);
        }
    }
}