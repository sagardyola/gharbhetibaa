using Gharbhetibaa.DAL;
using Gharbhetibaa.Models;
using Gharbhetibaa.Models.Images;
using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Gharbhetibaa.Controllers
{
    public class AccountController : Controller
    {
        private GharbhetibaaDbContext db = new GharbhetibaaDbContext();

        // GET: Account
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("Login");
        }

        //Register a new user
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //To register a new user
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "Gender, MobileNumber, DateOfBirth, AboutMe, ActivationCode")] RegisterVM userVMObj)
        {
            //check if user and email exists
            bool isUserValid = db.UserAccounts.Any(u => u.UserName.Equals(userVMObj.UserName));
            bool isEmailValid = db.UserAccounts.Any(u => u.Email.Equals(userVMObj.Email));
            if (ModelState.IsValid)
            {
                using (db)
                {
                    if (isUserValid == true)
                    {
                        TempData["UsernameChk"] = true;
                    } else if (isEmailValid == true) {
                        TempData["EmailChk"] = true;
                    }
                    else
                    {
                        UserAccount userObj = new UserAccount();
                        //Encrypt password
                        var hashedPassword = Crypto.HashPassword(userVMObj.Password);
                        var code = Guid.NewGuid();

                        userObj.Password = hashedPassword;
                        userObj.UserName = userVMObj.UserName.ToLower();
                        userObj.FirstName = userVMObj.FirstName;
                        userObj.LastName = userVMObj.LastName;
                        userObj.Address = userVMObj.Address;
                        userObj.Email = userVMObj.Email;
                        userObj.Status = true;
                        userObj.ActivationCode = code.ToString().Substring(0, 6);
                        userObj.IsVerified = false;

                        //save changes
                        db.UserAccounts.Add(userObj);
                        db.SaveChanges();
                        ModelState.Clear();
                        TempData["UserRegister"] = true;
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            return View();
        }

        //login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccount userObj)
        {
            using (db)
            {
                var usr = db.UserAccounts.Where(u => u.UserName == userObj.UserName.ToLower() && u.Status == true).FirstOrDefault();
                if (usr != null)
                {
                    //Decrypt password
                    var doesPasswordMatch = Crypto.VerifyHashedPassword(usr.Password, userObj.Password);
                    if(doesPasswordMatch == true)
                    {
                        //authenticate the user
                        FormsAuthentication.SetAuthCookie(userObj.UserName.ToLower(), false);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("LoginError", "Password is Incorrect.");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Username or Password is Incorrect.");
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //view user profile
        [Authorize]
        public ActionResult UserProfile(string UserName)
        {
            if (UserName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(UserName)).FirstOrDefault();
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        [Authorize]
        public ActionResult EditProfile()
        {
            string activeuser = User.Identity.Name;
            if (activeuser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserAccount usr = db.UserAccounts.Where(u => u.UserName.Equals(activeuser)).FirstOrDefault();
            if (usr == null)
            {
                return HttpNotFound();
            }
            return View(usr);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserAccount userObj)
        {
            string activeuser = User.Identity.Name;
            UserAccount activeObj = db.UserAccounts.Where(u => u.UserName.Equals(activeuser)).FirstOrDefault();
            int UserID = activeObj.UserID;

            var existingItems = db.UserAccounts.Find(UserID);
            //dont update imageURL if empty
            if (!string.IsNullOrEmpty(userObj.ImageURL))
            {
                existingItems.ImageURL = userObj.ImageURL;
            }

            existingItems.AboutMe = userObj.AboutMe;
            existingItems.DateOfBirth = userObj.DateOfBirth;
            existingItems.FirstName = userObj.FirstName;
            existingItems.LastName = userObj.LastName;
            existingItems.Address = userObj.Address;
            existingItems.Gender = userObj.Gender;
            existingItems.MobileNumber = userObj.MobileNumber;
            existingItems.Email = userObj.Email;

            using (db)
            {
                db.Entry(existingItems).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            TempData["SuccessEditProfile"] = true;

            return RedirectToAction("UserProfile", "Account", new { UserName = userObj.UserName });
        }

        //change user password
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(SettingsVM changeObj)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
                    var doesPasswordMatch = Crypto.VerifyHashedPassword(usr.Password, changeObj.ChangePasswordVM.OldPassword);
                    if (doesPasswordMatch == true)
                    {
                        var newHashedPassword = Crypto.HashPassword(changeObj.ChangePasswordVM.NewPassword);

                        usr.Password = newHashedPassword;
                        db.Entry(usr).CurrentValues.SetValues(usr);
                        db.SaveChanges();
                        TempData["SuccessPass"] = true;
                    }
                    else
                    {
                        TempData["FailPass"] = true;
                    }
                    ModelState.Clear();
                }
            }
            return RedirectToAction("Index","Settings");
        }

        //deactivate user
        [HttpPost]
        [Authorize]
        public ActionResult DeactivateUser()
        {
            var usr = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            usr.Status = false;
            db.Entry(usr).CurrentValues.SetValues(usr);
            db.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //Verification popup
        public ActionResult Verification()
        {
            //Current user
            var userObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            VerifyUserVM model = new VerifyUserVM();
            model.Email = userObj.Email;
            model.Subject = "Verification code";
            model.Message = "Dear " + userObj.FullName + ",\n\nYour verification code is " + userObj.ActivationCode + "\n\nKind regards,\nGharbhetibaa";
            return PartialView(model);
        }

        //Verification popup POST
        [HttpPost]
        public ActionResult Verification(VerifyUserVM model)
        {
            JsonResult result = new JsonResult();
            var userObj = db.UserAccounts.Where(u => u.UserName.Equals(User.Identity.Name.ToString())).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (model.VerifyCode == userObj.ActivationCode)
                {
                    userObj.IsVerified = true;
                    db.Entry(userObj).CurrentValues.SetValues(userObj);
                    db.SaveChanges();
                    result.Data = new { Success = true };
                }
                else
                {
                    result.Data = new { Success = false, Error = "Invalid code." };
                }
            }
            else
            {
                result.Data = new { Success = false, Error = "Unable to verify. Please enter valid values." };
            }
            return result;
        }

        //ForgotPassword Popup
        public ActionResult ForgotPassword()
        {
            return PartialView();
        }

        //Forgot Password Verify Email
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordVM model)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                var userObj = db.UserAccounts.Where(u => u.Email.Equals(model.VerifyEmail)).FirstOrDefault();
                if (userObj != null)
                {
                    var code = Guid.NewGuid().ToString();
                    userObj.ResetCode = code;
                    db.SaveChanges();

                    var verifyUrl = "/Account/ResetPassword?reset=" + code;
                    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

                    EmailVM emailObj = new EmailVM();
                    emailObj.Email = userObj.Email;
                    emailObj.Subject = "Reset your account";
                    emailObj.Message = "Dear " + userObj.FullName + ",\n\nPlease click the link below to reset your account.\n\n" + "<a href='" + link + "'>" + link + "</a>" + "\n\nKind regards,\nGharbhetibaa";

                    string emailBody = emailObj.Message.Replace("\n", "<br />");
                    bool resultEmail = false;
                    resultEmail = SendEmail(emailObj.Email, emailObj.Subject, emailBody);

                    return Json(new { Success = resultEmail }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    result.Data = new { Success = false, Error = "Invalid Email address." };
                }
            }
            else
            {
                result.Data = new { Success = false, Error = "Unable to verify. Please enter valid values." };
            }

            return result;
        }

        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public ActionResult ResetPassword(string reset)
        {
            var userObj = db.UserAccounts.Where(u => u.ResetCode.Equals(reset)).FirstOrDefault();
            ResetPasswordVM resetObj = new ResetPasswordVM();

            resetObj.ResetUser = userObj.UserName;
            return View(resetObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordVM resetObj)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var userObj = db.UserAccounts.Where(u => u.UserName.Equals(resetObj.ResetUser)).FirstOrDefault();

                    var newHashedPassword = Crypto.HashPassword(resetObj.NewPassword);

                    userObj.Password = newHashedPassword;
                    userObj.ResetCode = null;
                    db.Entry(userObj).CurrentValues.SetValues(userObj);
                    db.SaveChanges();

                    ModelState.Clear();
                    TempData["ResetPass"] = true;

                }
            }
            return RedirectToAction("Login", "Account");
        }

    }
}