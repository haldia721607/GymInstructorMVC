using BOL;
using GymInstructor.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymInstructor.Areas.AdminSection.Controllers
{
    public class DashboardController : BaseAdminSectionController
    {
        // GET: AdminSection/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult AdminDetail()
        {
            if (Session["CNAdminImage"] != null)
            {
                CommanDetails profileImage = (CommanDetails)Session["CNAdminImage"];
                var base64 = Convert.ToBase64String(profileImage.Image);
                var imgSrc = String.Format("data:" + profileImage.ImageType + ";base64,{0}", base64);
                ViewBag.ProfileImage = imgSrc;
            }
            ViewBag.UserName = Session[SessionObjects.CNUserName];
            return View();
        }
        public ActionResult UserEmail()
        {
            ViewBag.UserEmail = Session[SessionObjects.CNAdminEmail];
            return View();
        }
    }
}