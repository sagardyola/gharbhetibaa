using Gharbhetibaa.DAL;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using Gharbhetibaa.Models;

namespace Gharbhetibaa.Controllers
{
    public class HomeController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var itemsObj = from i in db.UserItemDetails
                           select i;

            var itemDateObj = itemsObj.ToList().Where(i => i.UserAccount.Status == true).Where(i => i.ItemDetail.Status == true).Select(i => i.ItemDetail);

            var model = new HomeVM();
            model.Showcase = db.UserFeatures.Select(x => x.ItemDetail);
            model.RecentAds = itemDateObj.OrderByDescending(e => e.ItemID).Take(6).ToList();
            model.CountItems = itemDateObj.Count();

            model.RentSale = itemDateObj.Where(e => e.ItemForID == 1 || e.ItemForID == 2).OrderByDescending(e => e.ItemID).Take(3).ToList();
            model.HomestayRoommate = itemDateObj.Where(e => e.ItemForID == 3 || e.ItemForID == 4).OrderByDescending(e => e.ItemID).Take(3).ToList();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult WhyUse()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult NeedHelp()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Terms()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(ContactUs contactUsObj)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    try
                    {
                        db.ContactUss.Add(contactUsObj);
                    }
                    finally
                    {
                        db.SaveChanges();
                        ModelState.Clear();
                        TempData["SuccessContactUs"] = true;
                    }
                }
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback(Feedback feedbackObj)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    try
                    {
                        db.Feedbacks.Add(feedbackObj);
                    }
                    finally
                    {
                        db.SaveChanges();
                        ModelState.Clear();
                        TempData["SuccessFeedback"] = true;
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Newsletter(Newsletter newsletterObj)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    try
                    {
                        db.Newsletters.Add(newsletterObj);
                    }
                    finally
                    {
                        db.SaveChanges();
                        ModelState.Clear();
                        TempData["SuccessNewsletter"] = true;
                    }
                }
            }
            return View();
        }
    }
}