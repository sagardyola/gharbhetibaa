using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    public class BookingController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: Booking
        public ActionResult ItemUser(int ItemID)
        {
            var bookedbyObj = db.BookingItems.Where(u => u.ItemID == ItemID).ToList();

            var itemObj = db.ItemDetails.Where(x => x.ItemID == ItemID).FirstOrDefault();

            string itemTitle = itemObj.ItemTitle + " at " + itemObj.LocationCity;

            ViewBag.BookItemCount = db.BookingItems.Where(u => u.ItemID == ItemID).Count();
            TempData["SuccessTitle"] = itemTitle;
            return PartialView(bookedbyObj);
        }

        public ActionResult SearchingForUser(int SearchingForID)
        {
            var bookedbyObj = db.BookingSearchings.Where(u => u.SearchingForID == SearchingForID).ToList();

            var itemObj = db.SearchingFors.Where(x => x.SearchingForID == SearchingForID).FirstOrDefault();

            string itemTitle = itemObj.Title + " at " + itemObj.LocationCity;

            ViewBag.BookSearchCount = db.BookingSearchings.Where(u => u.SearchingForID == SearchingForID).Count();
            TempData["SuccessTitle"] = itemTitle;
            return PartialView(bookedbyObj);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ItemBooking(int ItemID, BookingItem usrBooking)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount listedByUsr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
                    bool obj = db.BookingItems.Any(x => x.ItemID == ItemID && x.BookedBy == listedByUsr.UserID);
                    if (obj == false)
                    {
                        usrBooking.ItemID = ItemID;
                        usrBooking.BookedBy = listedByUsr.UserID;
                        db.BookingItems.Add(usrBooking);
                    }
                    else
                    {
                        BookingItem a = db.BookingItems.Where(x => x.ItemID == ItemID).Where(x => x.BookedBy == listedByUsr.UserID).FirstOrDefault();
                        db.BookingItems.Remove(a);
                    }
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("ItemDetails", "Item", new { ItemID = ItemID });
                }
            }
            return View(usrBooking);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SearchingBooking(int SearchingForID, BookingSearching usrBooking)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount listedByUsr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
                    //UserAccount itemUsr = db.UserItemDetails.Where(e => e.ItemID == ItemID).Select(e => e.UserAccount).FirstOrDefault();

                    bool obj = db.BookingSearchings.Any(x => x.SearchingForID == SearchingForID && x.BookedBy == listedByUsr.UserID);

                    if (obj == false)
                    {
                        usrBooking.SearchingForID = SearchingForID;
                        usrBooking.BookedBy = listedByUsr.UserID;
                        db.BookingSearchings.Add(usrBooking);
                    }
                    else
                    {
                        BookingSearching a = db.BookingSearchings.Where(x => x.SearchingForID == SearchingForID).Where(x => x.BookedBy == listedByUsr.UserID).FirstOrDefault();
                        db.BookingSearchings.Remove(a);
                    }

                    db.SaveChanges();
                    ModelState.Clear();

                    return RedirectToAction("SearchingForDetails", "SearchingFor", new { SearchingForID = SearchingForID });
                }
            }
            return View(usrBooking);
        }
    }
}