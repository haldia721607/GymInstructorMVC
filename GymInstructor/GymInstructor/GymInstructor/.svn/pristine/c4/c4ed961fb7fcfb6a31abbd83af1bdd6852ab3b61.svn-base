using BOL;
using GymInstructor.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace GymInstructor.Areas.UsersSection.Controllers
{
    public class DashboardController : BaseUserSectionController
    {
        // GET: UsersSection/Dashboard
        public ActionResult UserDashboard()
        {
            return View();
        }
        public ActionResult UserEmail()
        {
            ViewBag.UserEmail = Session[SessionObjects.CNUserEmail];
            return View();
        }
        public ActionResult UserDetails()
        {
            if (Session["CNUserImage"] != null)
            {
                CommanDetails profileImage = (CommanDetails)Session["CNUserImage"];
                var base64 = Convert.ToBase64String(profileImage.Image);
                var imgSrc = String.Format("data:" + profileImage.ImageType + ";base64,{0}", base64);
                ViewBag.ProfileImage = imgSrc;
            }
            ViewBag.UserName = Session[SessionObjects.CNUserName];
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(SingIn singIn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = Convert.ToInt32(Session[SessionObjects.CNUserId]);
                    bool checkPassword = userBs.ChangePassword(singIn.changePassword.CurrentPassword, singIn.changePassword.Password, singIn.changePassword.ConfirmPassword, userId);
                    TempData["Msg"] = "Password changed successfully";
                    return RedirectToAction("Logout", "Login", new { area = "Security" });
                }
                else
                {
                    TempData["Fail"] = "Please check current password";
                    return RedirectToAction("ChangePassword", "Dashboard", new { area = "UsersSection" });
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went worng.Please contact to administrator. " + ex;
                return RedirectToAction("ChangePassword", "Dashboard", new { area = "UsersSection" });
                throw;
            }
        }
    }
}