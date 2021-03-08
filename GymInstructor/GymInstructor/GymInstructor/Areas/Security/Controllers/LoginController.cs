using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BOL;
using BLL;
using GymInstructor.App_Start;
using System.Configuration;
using System.Text;
using System.Net.Mail;
using DAL;

namespace GymInstructor.Areas.Security.Controllers
{
    public class LoginController : BaseSecurityController
    {

        // GET: Security/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(SingIn singIn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var UserMembershipProvider = Membership.Providers["MyMembershipProvider"];
                    if (UserMembershipProvider.ValidateUser(singIn.login.Email, singIn.login.Password))
                    {
                        var getUser = securityAreaBs.userBs.GetUserByEmail(singIn.login.Email);
                        var getUserDetail = securityAreaBs.userBs.GetUserDetails(getUser.UserId);
                        if (getUserDetail != null)
                        {
                            if (getUserDetail.Image!=null&& getUserDetail.ImageName!=null&& getUserDetail.ImageType!=null)
                            {
                                CommanDetails profileImage = new CommanDetails();
                                profileImage.Image = getUserDetail.Image;
                                profileImage.ImageName = getUserDetail.ImageName;
                                profileImage.ImageType = getUserDetail.ImageType;
                                Session["CNUserImage"] = profileImage;
                            }
                            else
                            {
                                Session["CNUserImage"] = null;
                            }
                           
                        }
                        Session[SessionObjects.CNUserId] = getUser.UserId;
                        Session[SessionObjects.CNUserName] = getUser.Name;
                        Session[SessionObjects.CNUserEmail] = getUser.Email;
                        Session[SessionObjects.CNUserType] = Constant.USERTYPE.USER;
                        FormsAuthentication.SetAuthCookie(singIn.login.Email, false);
                        return RedirectToAction("UserDashboard", "Dashboard", new { area = "UsersSection" });
                    }
                    else
                    {
                        var getUserDetail = securityAreaBs.userBs.GetUserByEmail(singIn.login.Email);
                        if (getUserDetail != null)
                        {
                            if (getUserDetail.IsActive == false && getUserDetail.IsBlockByAdmin == false)
                            {
                                TempData["Msg"] = "Please verify account before login. Verification link alredy send your registerd email id.";
                            }
                            else if (getUserDetail.IsActive == true && getUserDetail.IsBlockByAdmin == true)
                            {
                                TempData["Msg"] = "Your account is banned. Please contact to administrator.";
                            }
                        }
                        return RedirectToAction("Login", "Login", new { area = "Security" });
                    }
                }
                else
                {
                    TempData["Msg"] = "Please enter email id or password.";
                    return RedirectToAction("Login", "Login", new { area = "Security" });
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Login failed " + ex;
                return RedirectToAction("Login", "Login", new { area = "Security" });
            }
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(SingIn singIn)
        {
            try
            {
                bool IsSendSccess = false;
                var resFindUsersByEmail = securityAreaBs.userBs.FindUsersByEmail(singIn.forgetPassword.Email);
                if (resFindUsersByEmail != null)
                {
                    if (resFindUsersByEmail.IsBlockByAdmin == false)
                    {
                        IsSendSccess = SendResetPasswordLink(resFindUsersByEmail.UserId, resFindUsersByEmail.Name, resFindUsersByEmail.Email);
                        TempData["Msg"] = "Password reset link send to your registerd email id .";
                    }
                    else
                    {
                        TempData["Fail"] = "Your account is banned. Please contact to administrator.";
                    }
                }
                else
                {
                    TempData["Fail"] = "Email id does not match. Please enter valid email id.";
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Forget password link send failed" + ex;
                throw;
            }
            return RedirectToAction("ForgetPassword", "Login", new { area = "Security" });
        }
        public ActionResult RessetPassword(string Id)
        {
            string userId = CommanFunction.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Id"]));
            TempData["forgetPassworduserId"] = Convert.ToInt32(userId);
            TempData.Keep("forgetPassworduserId");
            return View();
        }
        public ActionResult SuccessfullyResetPassword()
        {
            TempData["Msg"] = "Password successfully changed . Go to login page to login.";
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePassword(SingIn singIn)
        {
            try
            {
                int forgetPassworduserId = Convert.ToInt32(TempData["forgetPassworduserId"]);
                var updateUserPassword = securityAreaBs.userBs.UpdateUserPassword(singIn.resetPassword.Password, singIn.resetPassword.ConfirmPassword, forgetPassworduserId);
                if (updateUserPassword == true)
                {
                    TempData["Msg"] = "Password reset successfully. Please go to login page to login with new password";
                    return RedirectToAction("SuccessfullyResetPassword", "Login", new { area = "Security" });
                }
                else
                {
                    TempData["Fail"] = "Password reset unsuccessfully. Please contact to administrator";
                    return RedirectToAction("RessetPassword", "Login", new { area = "Security" });
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Password reset unsuccessfully. Please contact to administrator. " + ex;
                return RedirectToAction("RessetPassword", "Login", new { area = "Security" });
                throw;
            }
        }
        private bool SendResetPasswordLink(int userId, string name, string email)
        {
            bool IsSendSuccess = false;
            var key = CommanFunction.Encrypt(Convert.ToString(userId));
            string forgetPasswordLink = ConfigurationManager.AppSettings["WebRoot"] + "Security/Login/RessetPassword?Id=" + key;
            string to = email;
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.Subject = "Forget Password Notification from " + ConfigurationManager.AppSettings["AppName"];
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("Hello" + " " + name + ",");
            sbEmailBody.Append("<br /><br />Your forget password link.");
            sbEmailBody.Append("<br /><br />Please click <a href='" + forgetPasswordLink + "'>here</a> to create new password for your account.");
            sbEmailBody.Append("<br /><br />Thanks,");
            sbEmailBody.Append("<br /><br />" + ConfigurationManager.AppSettings["AppName"]);
            mailMessage.Body = sbEmailBody.ToString();
            IsSendSuccess = CommanFunction.SendEmail(mailMessage);
            return IsSendSuccess;
        }

        public ActionResult Logout()
        {
            SessionAbend();
            return RedirectToAction("Login", "Login", new { area = "Security" });
        }
        public void SessionAbend()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
        }
        public ActionResult ResendVerificationEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResendVerificationEmail(SingIn singIn)
        {
            try
            {
                bool IsSendSccess = false;
                var checkUserIsActive = securityAreaBs.userBs.UserIsActive(singIn.forgetPassword.Email);
                if (checkUserIsActive.IsActive == false && checkUserIsActive.IsBlockByAdmin == false)
                {
                    IsSendSccess = SendEmail(checkUserIsActive.UserId, checkUserIsActive.Email, checkUserIsActive.Name);
                    if (IsSendSccess == true)
                    {
                        TempData["Msg"] = "Account activation link send to your registerd email id . Please check your inbox.";
                    }
                    else
                    {
                        TempData["Fail"] = "Account activation link send faild. Please contack to administrator";
                    }
                }
                else if (checkUserIsActive.IsActive == false && checkUserIsActive.IsBlockByAdmin == true)
                {
                    TempData["Fail"] = "Your account is banned. Please contact to administrator.";
                }
                else if (checkUserIsActive.IsActive == true && checkUserIsActive.IsBlockByAdmin == true)
                {
                    TempData["Fail"] = "Your account is banned. Please contact to administrator.";
                }
                return RedirectToAction("ResendVerificationEmail", "Login", new { area = "Security" });
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Some thing went wrong . Please contact to administrator." + ex;
                return RedirectToAction("ResendVerificationEmail", "Login", new { area = "Security" });
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
            mailMessage.Subject = "Resend Verification Email " + ConfigurationManager.AppSettings["AppName"];
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