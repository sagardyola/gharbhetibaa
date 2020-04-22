using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.SearchingForItem;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    public class SearchingForController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: WantList
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SearchingForDetails(int SearchingForID)
        {
            var model = new SearchingForVM();
            model.UserAccount = db.UserSearchingFors.Where(e => e.SearchingForID == SearchingForID).Select(e => e.UserAccount).FirstOrDefault();
            model.SearchingFors = db.SearchingFors.Where(e => e.SearchingForID == SearchingForID).ToList();
            model.UserSearchingFors = db.UserSearchingFors;

            model.CommentSearchings = db.CommentSearchings.Where(e => e.SearchingForID == SearchingForID).OrderByDescending(e => e.CommentedOn).ToList();
            if (model == null)
            {
                return HttpNotFound();
            }

            UserAccount listedByUsr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            if (User.Identity.IsAuthenticated)
            {
                bool wishListObj = db.WishListSearchings.Any(x => x.SearchingForID == SearchingForID && x.WishListedBy == listedByUsr.UserID);
                bool bookingObj = db.BookingSearchings.Any(x => x.SearchingForID == SearchingForID && x.BookedBy == listedByUsr.UserID);

                if (wishListObj == true)
                {
                    ViewBag.WishListSuccess = true;
                }
                else
                {
                    ViewBag.WishListSuccess = false;
                }

                if (bookingObj == true)
                {
                    ViewBag.BookingSuccess = true;
                }
                else
                {
                    ViewBag.BookingSuccess = false;
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult AddSearchingFor()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult AddSearchingFor(SearchingFor wList)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
                    
                    //Item Code
                    var lastItem = db.SearchingFors.OrderByDescending(c => c.SearchingForID).FirstOrDefault();
                    if (lastItem == null)
                    {
                        wList.SearchingCode = "GB-SFC001";
                    }
                    else
                    {
                        //using string substring method to get the number of the last inserted Item's ItemID
                        wList.SearchingCode = "GB-SFC" + (Convert.ToInt32(lastItem.SearchingCode.Substring(6, lastItem.SearchingCode.Length - 6)) + 1).ToString("D3");
                    }
                    //End of itemcode

                    db.SearchingFors.Add(wList);

                    UserSearchingFor usrList = new UserSearchingFor
                    {
                        UserID = usr.UserID,
                        SearchingForID = wList.SearchingForID
                    };

                    db.UserSearchingFors.Add(usrList);

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


        [Authorize]
        public ActionResult EditSearchingFor(int? SearchingForID)
        {
            if (SearchingForID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SearchingFor searchingForObj = db.SearchingFors.Find(SearchingForID);
            if (searchingForObj == null)
            {
                return HttpNotFound();
            }

            return PartialView(searchingForObj);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult EditSearchingFor(int? SearchingForID, SearchingFor searchingForObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {

                db.Entry(searchingForObj).State = System.Data.Entity.EntityState.Modified;

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
        [Authorize]
        public ActionResult DeleteSearchingFor(int? SearchingForID)
        {
            Delete(SearchingForID);
            return RedirectToAction("MySearchingFors", "MyActivity");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteDashSearchingFor(int? SearchingForID)
        {
            Delete(SearchingForID);
            return RedirectToAction("SearchingFor", "Dashboard");
        }


        public void Delete(int? SearchingForID)
        {
            var searchingForObj = db.SearchingFors.Find(SearchingForID);

            //remove comment items
            db.CommentSearchings.RemoveRange(db.CommentSearchings.Where(x => x.SearchingForID == searchingForObj.SearchingForID));

            //remove wishlist items
            db.WishListSearchings.RemoveRange(db.WishListSearchings.Where(x => x.SearchingForID == searchingForObj.SearchingForID));

            //remove booking items
            db.BookingSearchings.RemoveRange(db.BookingSearchings.Where(x => x.SearchingForID == searchingForObj.SearchingForID));



            //Remove Item
            db.SearchingFors.Remove(searchingForObj);

            db.SaveChanges();
        }
    }
}