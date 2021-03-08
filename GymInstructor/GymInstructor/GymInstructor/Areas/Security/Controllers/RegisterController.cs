using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GymInstructor.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class RegisterController : BaseSecurityController
    {
        // GET: Security/Register
        public ActionResult NewUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                bool IsSendSccess = false;
                user.IsActive = false;
                user.IsBlockByAdmin = false;
                user.CreatedDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    int id = securityAreaBs.userBs.Newuser(user);
                    IsSendSccess = SendEmail(id, user.Email, user.Name);
                    if (IsSendSccess == true)
                    {
                        TempData["Msg"] = "Registration Successfully";
                    }
                    else
                    {
                        TempData["Fail"] = "Verify email send failed";
                    }
                    return RedirectToAction("NewUser", "Register", new { area = "Security" });
                }
                else
                {
                    TempData["Fail"] = "Registration failed";
                    return RedirectToAction("NewUser");
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Registration failed due to following reason - " + ex;
                return RedirectToAction("NewUser", "Register", new { area = "Security" });
                throw;
            }
        }
        private bool SendEmail(int userId, string email, string name)
        {
            bool IsSendSuccess = false;
            var key = CommanFunction.Encrypt(Convert.ToString(userId));
            string sVerificationLink = ConfigurationManager.AppSettings["VerfiyAccountRoot"] + "?Id=" + key;
            string to = email;
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.Subject = "Registration Notification from " + ConfigurationManager.AppSettings["AppName"];
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("Hello" + " " + name + ",");
            sbEmailBody.Append("<br /><br />You have registered successfully.You have to verify your account for login.");
            sbEmailBody.Append("<br /><br />Please click <a href='" + sVerificationLink + "'>here</a> to activate your account.");
            sbEmailBody.Append("<br /><br />Thanks,");
            sbEmailBody.Append("<br /><br />" + ConfigurationManager.AppSettings["AppName"]);
            mailMessage.Body = sbEmailBody.ToString();
            IsSendSuccess = CommanFunction.SendEmail(mailMessage);
            return IsSendSuccess;
        }
    }
}