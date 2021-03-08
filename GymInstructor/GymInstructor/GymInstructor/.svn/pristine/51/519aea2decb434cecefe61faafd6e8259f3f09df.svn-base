using BOL;
using GymInstructor.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymInstructor.Areas.AdminSection.Controllers
{
    public class ProfileController : BaseAdminSectionController
    {
        // GET: AdminSection/Profile
        public ActionResult UpdateProfile()
        {
            try
            {
                int adminId = Convert.ToInt32(Session[SessionObjects.CNAdminId]);
                var getAdminDetail = adminArearBs.adminBs.GetAdminDetailById(adminId);
                if (getAdminDetail!=null)
                {
                    if (Session["CNAdminImage"] != null)
                    {
                        CommanDetails profileImage = (CommanDetails)Session["CNAdminImage"];
                        var base64 = Convert.ToBase64String(profileImage.Image);
                        var imgSrc = String.Format("data:" + profileImage.ImageType + ";base64,{0}", base64);
                        ViewBag.ProfileImage = imgSrc;
                    }
                    else
                    {
                        ViewBag.ProfileImage = null;
                        ViewBag.DummyImage = Url.Content("~/Content/admin/img/DummyImage.jpg");
                    }
                    return View(getAdminDetail);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(UpdateAdmin updateAdmin, HttpPostedFileBase postedFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int adminId = Convert.ToInt32(Session[SessionObjects.CNAdminId]);
                    bool updateAdminProfile = adminArearBs.adminBs.UpdateAdminProfile(adminId,updateAdmin.updateAdminDetail, postedFile);
                    if (updateAdminProfile==true)
                    {
                        var getAdminProfileImage = adminArearBs.adminBs.GetProfileImage(adminId);
                        if (getAdminProfileImage.Image!=null)
                        {
                            Session["CNAdminImage"] = null;
                            CommanDetails commanDetails = new CommanDetails();
                            commanDetails.Image = getAdminProfileImage.Image;
                            commanDetails.ImageName = getAdminProfileImage.ImageName;
                            commanDetails.ImageType = getAdminProfileImage.ImageType;
                            commanDetails.Email = getAdminProfileImage.Email;
                            commanDetails.Name = getAdminProfileImage.Name;
                            Session["CNAdminImage"] = commanDetails;
                        }
                        Session[SessionObjects.CNAdminEmail] = null;
                        Session[SessionObjects.CNAdminName] = null;
                        Session[SessionObjects.CNAdminEmail] = getAdminProfileImage.Email;
                        Session[SessionObjects.CNAdminName] = getAdminProfileImage.Name;
                    }
                    TempData["Msg"] = "Admin detail update successfully";
                }
                return RedirectToAction("UpdateProfile", "Profile", new { area = "AdminSection" });
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went wrong ." + ex;
                return RedirectToAction("UpdateProfile", "Profile", new { area = "AdminSection" });
                throw;
            }
        }
    }
}