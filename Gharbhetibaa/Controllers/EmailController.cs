using Gharbhetibaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Gharbhetibaa.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index(string Email)
        {
            EmailVM model = new EmailVM();
            model.Email = Email;
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendEmailToUser(EmailVM emailObj)
        {
            JsonResult resultError = new JsonResult();
            if (ModelState.IsValid)
            {
                string emailBody = emailObj.Message.Replace("\n", "<br />");
                bool result = false;
                result = SendEmail(emailObj.Email, emailObj.Subject, emailBody);

                return Json(new { Success = result }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                resultError.Data = new { Success = false, Error = "Unable to send. Please enter valid values." };
                return resultError;
            }
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
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}