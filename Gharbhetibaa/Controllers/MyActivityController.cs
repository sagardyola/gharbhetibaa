using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.SearchingForItem;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    [Authorize]
    public class MyActivityController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();
        readonly int pageSize = 8;
        // GET: MyActivity
        public ActionResult Index()
        {
            ItemData model = new ItemData();
            model.UserAccount = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            return View(model);
        }

        public ActionResult Home()
        {
            UserAccount activeUserObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            var itmAdd = db.UserItemDetails.Where(x => x.UserID == activeUserObj.UserID).Select(x => x.ItemDetail).ToList();
            var searchAdd = db.UserSearchingFors.Where(x => x.UserID == activeUserObj.UserID).Select(x => x.SearchingFor).ToList();

            var itmBook = db.BookingItems.Where(e => e.BookedBy == activeUserObj.UserID).Select(e => e.ItemDetail).ToList();
            var searchBook = db.BookingSearchings.Where(e => e.BookedBy == activeUserObj.UserID).Select(e => e.SearchingFor).ToList();

            var itmWish = db.WishListItems.Where(e => e.WishListedBy == activeUserObj.UserID).Select(e => e.ItemDetail).ToList();
            var searchWish = db.WishListSearchings.Where(e => e.WishListedBy == activeUserObj.UserID).Select(e => e.SearchingFor).ToList();


            MyActivityVM model = new MyActivityVM();

            model = new MyActivityVM()
            {
                ItemsAdded = itmAdd.Count(),
                SearchingAdded = searchAdd.Count(),
                
                ItemsWishListed = itmWish.Count(),
                SearchingWishListed = searchWish.Count(),

                ItemsBooked = itmBook.Count(),
                SearchingBooked = searchBook.Count()
            };
            return PartialView(model);
        }

        public ActionResult MyItems(int? pageNo)
        {
            var model = new ItemData();

            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            if (usr != null)
            {
                var itemObj = db.UserItemDetails.Where(e => e.UserAccount.UserID == usr.UserID).Select(e => e.ItemDetail).AsEnumerable();

                int count = itemObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                itemObj = itemObj.OrderByDescending(x => x.ItemID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.ItemDetails = itemObj;
            }


            model.UserAccount = usr;
            return PartialView(model);
        }

        public ActionResult MySearchingFors(int? pageNo)
        {
            var model = new SearchingForVM();

            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {
                var searchObj = db.UserSearchingFors.Where(e => e.UserAccount.UserID == usr.UserID).Select(e => e.SearchingFor).AsEnumerable();

                int count = searchObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                searchObj = searchObj.OrderByDescending(x => x.SearchingForID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.SearchingFors = searchObj;
            }
            model.UserAccount = usr;
            return PartialView(model);
        }

        public ActionResult WishListItems(int? pageNo)
        {
            var model = new ItemData();

            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {
                var itemObj = db.WishListItems.Where(e => e.WishListedBy == usr.UserID).Select(e => e.ItemDetail).AsEnumerable();
                int count = itemObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                itemObj = itemObj.OrderByDescending(x => x.ItemID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.ItemDetails = itemObj;
            }
            return PartialView(model);
        }

        public ActionResult WishListSearchingFors(int? pageNo)
        {
            var model = new SearchingForVM();

            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {

                var searchObj = db.WishListSearchings.Where(e => e.WishListedBy == usr.UserID).Select(e => e.SearchingFor).AsEnumerable();
                int count = searchObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                searchObj = searchObj.OrderByDescending(x => x.SearchingForID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.SearchingFors = searchObj;
            }
            return PartialView(model);
        }


        public ActionResult BookingItems(int? pageNo)
        {
            var model = new ItemData();

            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {

                var itemObj = db.BookingItems.Where(e => e.BookedBy == usr.UserID).Select(e => e.ItemDetail).AsEnumerable();
                int count = itemObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                itemObj = itemObj.OrderByDescending(x => x.ItemID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.ItemDetails = itemObj;
            }
            return PartialView(model);
        }

        public ActionResult BookingSearchingFors(int? pageNo)
        {
            var model = new SearchingForVM();

            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {

                var searchObj = db.BookingSearchings.Where(e => e.BookedBy == usr.UserID).Select(e => e.SearchingFor).AsEnumerable();
                int count = searchObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                searchObj = searchObj.OrderByDescending(x => x.SearchingForID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.SearchingFors = searchObj;
            }
            return PartialView(model);
        }

        public ActionResult CommentItems(int? pageNo)
        {
            var model = new ItemData();

            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {

                var itemObj = db.CommentItems.Where(e => e.UserID == usr.UserID).Select(e => e.ItemDetail).Distinct().AsEnumerable();
                int count = itemObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                itemObj = itemObj.OrderByDescending(x => x.ItemID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.ItemDetails = itemObj;
            }
            return PartialView(model);
        }

        public ActionResult CommentSearchingFors(int? pageNo)
        {
            var model = new SearchingForVM();

            //To Find userid
            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (usr != null)
            {

                var searchObj = db.CommentSearchings.Where(e => e.UserID == usr.UserID).Select(e => e.SearchingFor).Distinct().AsEnumerable();
                int count = searchObj.Count();
                pageNo = pageNo ?? 1;
                var skipCount = (pageNo.Value - 1) * pageSize;

                searchObj = searchObj.OrderByDescending(x => x.SearchingForID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.SearchingFors = searchObj;
            }
            return PartialView(model);
        }
    }
}