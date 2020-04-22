using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Item;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Gharbhetibaa.Models.Images;
using System.Net;

namespace Gharbhetibaa.Controllers
{
    public class ItemController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: Item
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ItemDetails(int? ItemID)
        {
            if (ItemID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new ItemData();
            model.UserAccount = db.UserItemDetails.Where(e => e.ItemID == ItemID).Select(e => e.UserAccount).FirstOrDefault();
            model.ItemDetails = db.ItemDetails.Where(e => e.ItemID == ItemID).ToList();
            model.UserItemDetails = db.UserItemDetails;

            model.CommentItems = db.CommentItems.Where(e => e.ItemID == ItemID).OrderByDescending(e => e.CommentedOn).ToList();

            if (model == null)
            {
                return HttpNotFound();
            }

            UserAccount listedByUsr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();


            if (User.Identity.IsAuthenticated)
            {
                bool wishListObj = db.WishListItems.Any(x => x.ItemID == ItemID && x.WishListedBy == listedByUsr.UserID);
                bool bookingObj = db.BookingItems.Any(x => x.ItemID == ItemID && x.BookedBy == listedByUsr.UserID);
                bool featureObj = db.UserFeatures.Any(x => x.ItemID == ItemID);

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

                if (featureObj == true)
                {
                    ViewBag.FeatureSuccess = true;
                }
                else
                {
                    ViewBag.FeatureSuccess = false;
                }
            }
            return View(model);
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


        [Authorize]
        public ActionResult AddItem()
        {
            PopulateItemForDropDownList();
            PopulateItemTypeDropDownList();
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult AddItem(string ItemPic, ItemDetail itemObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

                    //Item Code
                    var lastItem = db.ItemDetails.OrderByDescending(c => c.ItemID).FirstOrDefault();
                    if (lastItem == null)
                    {
                        itemObj.ItemCode = "GB-ITM001";
                    }
                    else
                    {
                        //using string substring method to get the number of the last inserted Item's ItemID
                        itemObj.ItemCode = "GB-ITM" + (Convert.ToInt32(lastItem.ItemCode.Substring(6, lastItem.ItemCode.Length - 6)) + 1).ToString("D3");
                    }
                    //End of itemcode

                    db.ItemDetails.Add(itemObj);
                    UserItemDetail usrItem = new UserItemDetail
                    {
                        UserID = usr.UserID,
                        ItemID = itemObj.ItemID
                    };
                    db.UserItemDetails.Add(usrItem);

                    //Image
                    var pictureIDs = ItemPic.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                    itemObj.PictureItems = new List<PictureItem>();
                    itemObj.PictureItems.AddRange(pictureIDs.Select(x => new PictureItem() { ImageID = x }).ToList());
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

        [Authorize]
        public ActionResult EditItem(int? ItemID)
        {
            if (ItemID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ItemDetail itemObj = db.ItemDetails.Find(ItemID);
            if (itemObj == null)
            {
                return HttpNotFound();
            }
            PopulateItemForDropDownList(itemObj.ItemForID);
            PopulateItemTypeDropDownList(itemObj.ItemTypeID);
            return PartialView(itemObj);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult EditItem(string ItemPic, int? ItemID, ItemDetail itemObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                //db.Entry(itemObj).State = System.Data.Entity.EntityState.Modified;
                db.Entry(itemObj.LookingFor).State = System.Data.Entity.EntityState.Modified;
                db.Entry(itemObj.Lifestyle).State = System.Data.Entity.EntityState.Modified;
                db.Entry(itemObj.RoomDetail).State = System.Data.Entity.EntityState.Modified;
                db.Entry(itemObj.OtherFeature).State = System.Data.Entity.EntityState.Modified;

                //if(!string.IsNullOrEmpty(itemObj.PictureItems))
                //{ 
                //Image
                var pictureIDs = ItemPic.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                itemObj.PictureItems = new List<PictureItem>();
                itemObj.PictureItems.AddRange(pictureIDs.Select(x => new PictureItem() { ItemID = itemObj.ItemID, ImageID = x }).ToList());
                //}
                
                var existingItems = db.ItemDetails.Find(ItemID);
                db.PictureItems.RemoveRange(existingItems.PictureItems);
                db.Entry(existingItems).CurrentValues.SetValues(itemObj);
                db.PictureItems.AddRange(itemObj.PictureItems);
                db.SaveChanges();

                PopulateItemForDropDownList(itemObj.ItemForID);
                PopulateItemTypeDropDownList(itemObj.ItemTypeID);
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
        public ActionResult DeleteItem(int? ItemID)
        {
            Delete(ItemID);

            return RedirectToAction("MyItems", "MyActivity");
            //return RedirectToAction("Index", "PropertyManagement", new { PropertyID = PropertyID, TenantID = TenantID });
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteDashItem(int? ItemID)
        {
            Delete(ItemID);
            return RedirectToAction("Items", "Dashboard");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteFeatItem(int? ItemID)
        {
            Delete(ItemID);
            return RedirectToAction("FeaturedItems", "Dashboard");
        }

        public void Delete(int? ItemID)
        {
            var itemObj = db.ItemDetails.Find(ItemID);

            //remove picture


            //remove comment items
            db.CommentItems.RemoveRange(db.CommentItems.Where(x => x.ItemID == itemObj.ItemID));

            //remove wishlist items
            db.WishListItems.RemoveRange(db.WishListItems.Where(x => x.ItemID == itemObj.ItemID));

            //remove booking items
            db.BookingItems.RemoveRange(db.BookingItems.Where(x => x.ItemID == itemObj.ItemID));

            //remove looking fors
            LookingFor lookingForObj = db.LookingFors.Where(x => x.LookingForID == itemObj.LookingForID).Single();
            db.LookingFors.Remove(lookingForObj);

            //remove room details
            RoomDetail roomDetailObj = db.RoomDetails.Where(x => x.RoomDetailID == itemObj.RoomDetailID).Single();
            db.RoomDetails.Remove(roomDetailObj);

            //remove lifestyle
            Lifestyle lifestyleObj = db.Lifestyles.Where(x => x.LifestyleID == itemObj.LifestyleID).Single();
            db.Lifestyles.Remove(lifestyleObj);

            //remove other features
            OtherFeature otherFeatureObj = db.OtherFeatures.Where(x => x.OtherFeatureID == itemObj.OtherFeatureID).Single();
            db.OtherFeatures.Remove(otherFeatureObj);

            //Remove Item
            db.ItemDetails.Remove(itemObj);

            db.SaveChanges();
        }

    }
}