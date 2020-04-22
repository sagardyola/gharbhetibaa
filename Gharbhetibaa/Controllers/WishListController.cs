using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.WishList;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    //Add to wishlist
    public class WishListController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: Bookmark
        public ActionResult Index()
        {
            var viewModel = new ItemData();
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {
                viewModel.UserAccount = usr;
                viewModel.ItemDetails = db.WishListItems.Where(e => e.WishListedBy == usr.UserID).Select(e => e.ItemDetail).ToList();
            }
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ItemWishList(int ItemID, WishListItem usrWishList)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount listedByUsr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
                    bool obj = db.WishListItems.Any(x => x.ItemID == ItemID && x.WishListedBy == listedByUsr.UserID);
                    if (obj == false)
                    {
                        usrWishList.ItemID = ItemID;
                        usrWishList.WishListedBy = listedByUsr.UserID;
                        db.WishListItems.Add(usrWishList);
                    }
                    else
                    {
                        WishListItem a = db.WishListItems.Where(x => x.ItemID == ItemID).Where(x => x.WishListedBy == listedByUsr.UserID).FirstOrDefault();
                        db.WishListItems.Remove(a);
                    }

                    db.SaveChanges();
                    ModelState.Clear();

                    return RedirectToAction("ItemDetails", "Item", new { ItemID = ItemID });
                }
            }
            return View(usrWishList);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SearchingWishList(int SearchingForID, WishListSearching usrWishList)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount listedByUsr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
                    bool obj = db.WishListSearchings.Any(x => x.SearchingForID == SearchingForID && x.WishListedBy == listedByUsr.UserID);
                    if (obj == false)
                    {
                        usrWishList.SearchingForID = SearchingForID;
                        usrWishList.WishListedBy = listedByUsr.UserID;
                        db.WishListSearchings.Add(usrWishList);
                    }
                    else
                    {
                        WishListSearching a = db.WishListSearchings.Where(x => x.SearchingForID == SearchingForID).Where(x => x.WishListedBy == listedByUsr.UserID).FirstOrDefault();
                        db.WishListSearchings.Remove(a);
                    }

                    db.SaveChanges();
                    ModelState.Clear();

                    return RedirectToAction("SearchingForDetails", "SearchingFor", new { SearchingForID = SearchingForID });
                }
            }
            return View(usrWishList);
        }
    }
}