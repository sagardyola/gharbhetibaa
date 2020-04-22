using Gharbhetibaa.DAL;
using Gharbhetibaa.Models.SearchingForItem;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    public class ExploreController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();
        readonly int pageSize = 10;


        public ActionResult ExploreDetails()
        {
            return View();
        }

        private void PopulateItemForDropDownList(object selectedItemFor = null)
        {
            var itemForQuery = from d in db.ItemFors
                               select d;
            ViewBag.ItemForID = new SelectList(itemForQuery, "ItemForID", "ItemForTitle", selectedItemFor);
        }

        private void PopulateItemTypeDropDownList(object selectedItemType = null)
        {
            var itemTypeQuery = from d in db.ItemTypes
                                select d;
            ViewBag.ItemTypeID = new SelectList(itemTypeQuery, "ItemTypeID", "ItemTypeTitle", selectedItemType);
        }

        // GET: Explore
        [AllowAnonymous]
        public ActionResult Index(int? ItemForID, int? ItemTypeID, string SearchText, string SearchType, int? pageNo)
        {
            PopulateItemForDropDownList();
            PopulateItemTypeDropDownList();
            ExploreVM model = new ExploreVM();

            if (SearchType == "Item" || SearchType == null)
            {
                var itemsObj = db.UserItemDetails.AsEnumerable().Where(i => i.UserAccount.Status == true).Where(i => i.ItemDetail.Status == true).Select(i => i.ItemDetail);
                if (!String.IsNullOrEmpty(SearchText))
                {
                    itemsObj = itemsObj.Where(s => s.Location.ToLower().Contains(SearchText.ToLower()) || s.City.ToLower().Contains(SearchText.ToLower()));
                }

                if (ItemForID.HasValue && ItemForID.Value > 0)
                {
                    itemsObj = itemsObj.Where(e => e.ItemForID == ItemForID);
                }

                if (ItemTypeID.HasValue && ItemTypeID.Value > 0)
                {
                    itemsObj = itemsObj.Where(e => e.ItemTypeID == ItemTypeID);
                }

                int count = itemsObj.Count();
                pageNo = pageNo ?? 1;

                var skipCount = (pageNo.Value - 1) * pageSize;

                itemsObj = itemsObj.OrderByDescending(x => x.ItemID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.Count = count;
                model.ItemDetails = itemsObj.ToList();
                model.SearchText = SearchText;

                model.ItemForID = ItemForID ?? default(int);
                model.ItemTypeID = ItemTypeID ?? default(int);
                model.UserItemDetails = db.UserItemDetails;

                model.SearchType = "Item";
            } else if (SearchType == "SearchingFor")
            {
                var srch = db.UserSearchingFors.AsEnumerable().Where(i => i.UserAccount.Status == true).Where(i => i.SearchingFor.Status == true).Select(i => i.SearchingFor);

                if (!String.IsNullOrEmpty(SearchText))
                {
                    srch = srch.Where(s => s.Location.ToLower().Contains(SearchText.ToLower()) || s.City.ToLower().Contains(SearchText.ToLower()));
                }

                int count = srch.Count();
                pageNo = pageNo ?? 1;
                //pageNo = pageNo.HasValue ? pageNo.Value : 1;

                var skipCount = (pageNo.Value - 1) * pageSize;

                srch = srch.OrderByDescending(x => x.SearchingForID).Skip(skipCount).Take(pageSize);

                model.Pager = new Pager(count, pageNo, pageSize);
                model.Count = count;
                model.SearchingFors = srch.ToList();
                model.SearchText = SearchText;
                model.UserSearchingFors = db.UserSearchingFors;

                model.SearchType = "SearchingFor";
            }
            return View(model);
            
        }
    }
}