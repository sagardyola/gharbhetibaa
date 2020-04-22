using Gharbhetibaa.DAL;
using Gharbhetibaa.Models.Images;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    public class PictureController : Controller
    {
        GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: Picture
        public ActionResult Index()
        {
            return View();
        }

        //Upload Multiple Images
        [HttpPost]
        public JsonResult PictureItem()
        {
            JsonResult result = new JsonResult();
            List<object> picturesJSON = new List<object>();

            var pictures = Request.Files;

            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var path = Server.MapPath("~/images/Item/") + fileName;

                picture.SaveAs(path);

                var pictureObj = new Picture();
                pictureObj.ImageLocation = fileName;

                db.Pictures.Add(pictureObj);
                db.SaveChanges();

                picturesJSON.Add(new { ID = pictureObj.ImageID, pictureURL = fileName });
            }

            result.Data = picturesJSON;
            return result;
        }

        //Upload Multiple Images
        [HttpPost]
        public JsonResult PictureContractDoc()
        {
            JsonResult result = new JsonResult();
            List<object> picturesJSON = new List<object>();

            var pictures = Request.Files;

            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var path = Server.MapPath("~/images/ContractDocuments/") + fileName;

                picture.SaveAs(path);

                var pictureObj = new Picture();
                pictureObj.ImageLocation = fileName;

                db.Pictures.Add(pictureObj);
                db.SaveChanges();

                picturesJSON.Add(new { ID = pictureObj.ImageID, pictureURL = fileName });
            }

            result.Data = picturesJSON;
            return result;
        }

        //Upload Profile picture (Single Image)
        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/images/UserAccount/"), fileName);
                file.SaveAs(path);
                result.Data = new { Success = true, ImageURL = fileName };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }

    }
}