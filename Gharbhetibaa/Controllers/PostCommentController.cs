using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.PostComment;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    public class PostCommentController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: PostComment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ItemComment()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult ItemComment(int ItemID, ItemData modelObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    try
                    {
                        UserAccount activeUserObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

                        modelObj.CommentItem.UserID = activeUserObj.UserID;
                        modelObj.CommentItem.ItemID = ItemID;
                        db.CommentItems.Add(modelObj.CommentItem);

                        db.SaveChanges();
                        ModelState.Clear();
                        TempData["Success"] = "Commented!!!";
                        result.Data = new { Success = true };

                    }
                    catch (Exception ex)
                    {
                        result.Data = new { Success = false, Message = ex.Message };
                    }
                }
            }
            return result;
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteItemComment(int? ItemID, int? CommentID)
        {
            var itemObj = db.CommentItems.Where(i => i.CommentID == CommentID).FirstOrDefault();
            db.CommentItems.Remove(itemObj);
            db.SaveChanges();
            return RedirectToAction("ItemDetails", "Item", new { ItemID = ItemID });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult SearchingComment(int SearchingForID, SearchingForVM modelObj)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                using (db)
                {
                    try
                    {
                        UserAccount activeUserObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();

                        modelObj.CommentSearching.UserID = activeUserObj.UserID;
                        modelObj.CommentSearching.SearchingForID = SearchingForID;

                        db.CommentSearchings.Add(modelObj.CommentSearching);

                        db.SaveChanges();
                        ModelState.Clear();
                        TempData["Success"] = "Commented!!!";

                        result.Data = new { Success = true };
                    }
                    catch (Exception ex)
                    {
                        result.Data = new { Success = false, Message = ex.Message };
                    }
                }
            }
            return result;
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteSearchingComment(int? SearchingForID, int? CommentID)
        {
            var srchObj = db.CommentSearchings.Where(i => i.CommentID == CommentID).FirstOrDefault();
            db.CommentSearchings.Remove(srchObj);
            db.SaveChanges();
            return RedirectToAction("SearchingForDetails", "SearchingFor", new { SearchingForID = SearchingForID });
        }
    }
}