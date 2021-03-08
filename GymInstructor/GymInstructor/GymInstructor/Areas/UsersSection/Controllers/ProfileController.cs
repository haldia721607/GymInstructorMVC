using BOL;
using DAL;
using GymInstructor.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymInstructor.Areas.UsersSection.Controllers
{
    public class ProfileController : BaseUserSectionController
    {
        // GET: UsersSection/Profile
        public ActionResult UserProfile()
        {
            try
            {
                int userId = Convert.ToInt32(Session[SessionObjects.CNUserId]);
                var getUserDetails = userBs.GetUserDetailById(userId);
                ViewBag.CountryList = new SelectList(userBs.CountryList(), "ID", "Name");
                ViewBag.GenderList = new SelectList(userBs.GenderList(), "GenderId", "GenderName");
                if (getUserDetails != null)
                {
                    int iCountryId = Convert.ToInt32(getUserDetails.updateUserDetail.CountryId);
                    int iStateId = Convert.ToInt32(getUserDetails.updateUserDetail.StateId);
                    List<State> stateList = userBs.StateList(iCountryId);
                    ViewBag.StateList = new SelectList(stateList, "ID", "Name");
                    List<City> cityList = userBs.CityList(iStateId);
                    ViewBag.CityList = new SelectList(cityList, "ID", "Name");
                    if (Session["CNUserImage"] != null)
                    {
                        CommanDetails profileImage = (CommanDetails)Session["CNUserImage"];
                        var base64 = Convert.ToBase64String(profileImage.Image);
                        var imgSrc = String.Format("data:" + profileImage.ImageType + ";base64,{0}", base64);
                        ViewBag.ProfileImage = imgSrc;
                    }
                    else
                    {
                        ViewBag.ProfileImage = null;
                        ViewBag.DummyImage = Url.Content("~/Content/admin/img/DummyImage.jpg");
                    }
                    return View(getUserDetails);
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
        public ActionResult GetState(int CountryId)
        {
            try
            {
                List<State> stateList = userBs.StateList(CountryId);
                ViewBag.StateList = new SelectList(stateList, "ID", "Name");
                //Using j-query  @onchange = "javascript:GetState(this.value);" seperate fuction and bind dynamic without page reload
                //Function call in dropdownlistfor (@onchange = "javascript:GetState(this.value);") 
                return Json(ViewBag.StateList);
                //for use partial view to bind drop down but not work in edit mode (not bind and not selected value set problem). Only work  in insert not working ,and editing time not get selected value or not bind table set failed
                //return PartialView("DisplayState");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetCity(int stateId)
        {
            try
            {
                List<City> cityList = userBs.CityList(stateId);
                ViewBag.CityList = new SelectList(cityList, "ID", "Name");
                //Using j-query  @onchange = "javascript:GetState(this.value);" seperate fuction and bind dynamic without page reload
                //Function call in dropdownlistfor (@onchange = "javascript:GetCity(this.value);") 
                return Json(ViewBag.CityList);
                //for use partial view to bind drop down but not work in edit mode (not bind and not selected value set problem). Only work  in insert not working ,and editing time not get selected value or not bind table set failed
                //return PartialView("DisplyCity");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserDetails(ShowUserDetails userDetail, HttpPostedFileBase postedFile)
        {
            try
            {
                int userId = Convert.ToInt32(Session[SessionObjects.CNUserId]); 
                var getUserDetails = userBs.checkUserIdExist(userId);
                if (getUserDetails!=0)
                {
                    //Update
                    if (ModelState.IsValid)
                    {
                        bool updateUserDetail = userBs.UpdateUserDetail(userId, userDetail.updateUserDetail, userDetail.updateUser, postedFile);
                        if (updateUserDetail==true)
                        {
                            var getUserProfileImage = userBs.GetUserProfileImage(userId);
                            if (getUserProfileImage!=null)
                            {
                                Session["CNUserImage"] = null;
                                Session[SessionObjects.CNUserName] = null;
                                Session[SessionObjects.CNUserEmail] = null;
                                CommanDetails profileImage = new CommanDetails();
                                profileImage.Image = getUserProfileImage.Image;
                                profileImage.ImageName = getUserProfileImage.ImageName;
                                profileImage.ImageType = getUserProfileImage.ImageType;
                                Session[SessionObjects.CNUserName] = getUserProfileImage.Name;
                                Session[SessionObjects.CNUserEmail] = getUserProfileImage.Email;
                                Session["CNUserImage"] = profileImage;
                            }
                            TempData["Msg"] = "User detail update successfully";
                        }
                    }
                }
                else
                {
                    //Insert
                    if (ModelState.IsValid)
                    {
                        bool insertUserDetail = userBs.InsertUserDetail(userId,userDetail.updateUserDetail, userDetail.updateUser, postedFile);
                        if (insertUserDetail==true)
                        {
                            var getUserProfileImage = userBs.GetUserProfileImage(userId);
                            if (getUserProfileImage != null)
                            {
                                Session["CNUserImage"] = null;
                                CommanDetails profileImage = new CommanDetails();
                                profileImage.Image = getUserProfileImage.Image;
                                profileImage.ImageName = getUserProfileImage.ImageName;
                                profileImage.ImageType = getUserProfileImage.ImageType;
                                Session["CNUserImage"] = profileImage;
                            }
                            TempData["Msg"] = "User detail update successfully";
                        }
                    }
                }
                return RedirectToAction("UserProfile", "Profile", new { area= "UsersSection" });
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went wrong . Please contact to administrator." + ex;
                return RedirectToAction("UserProfile", "Profile", new { area = "UsersSection" });
                throw;
            }
        }
    }
}