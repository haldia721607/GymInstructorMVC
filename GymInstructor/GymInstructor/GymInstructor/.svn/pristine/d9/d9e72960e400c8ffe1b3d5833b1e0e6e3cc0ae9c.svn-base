using BOL;
using GymInstructor.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GymInstructor.Areas.Security.Controllers
{
    public class AdminLoginController : BaseSecurityController
    {
        // GET: Security/AdminLogin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SingIn singIn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var AdminMembershipProvider = Membership.Providers["AdminMembershipProvider"];
                    if (AdminMembershipProvider.ValidateUser(singIn.login.Email, singIn.login.Password))
                    {
                        var resAdminLogin = securityAreaBs.adminBs.GetAdminByEmail(singIn.login.Email);
                        if (resAdminLogin.Image!=null&& resAdminLogin.ImageName!=null&& resAdminLogin.ImageType!=null)
                        {
                            CommanDetails commanDetails = new CommanDetails();
                            commanDetails.Image = resAdminLogin.Image;
                            commanDetails.ImageName = resAdminLogin.ImageName;
                            commanDetails.ImageType = resAdminLogin.ImageType;
                            Session["CNAdminImage"] = commanDetails;
                        }
                        else
                        {
                            Session["CNAdminImage"] = null;
                        }
                        Session[SessionObjects.CNAdminId] = resAdminLogin.AdminId;
                        Session[SessionObjects.CNAdminName] = resAdminLogin.Name;
                        Session[SessionObjects.CNAdminEmail] = resAdminLogin.Email;
                        Session[SessionObjects.CNUserType] = Constant.USERTYPE.ADMIN;
                        FormsAuthentication.SetAuthCookie(singIn.login.Email, false);
                        return RedirectToAction("Dashboard", "Dashboard", new { area = "AdminSection" });
                    }
                    else
                    {
                        TempData["Msg"] = "Please check user name or password.";
                        return RedirectToAction("Login", "AdminLogin", new { area = "Security" });
                    }
                }
                else
                {
                    TempData["Msg"] = "Please enter user name or password.";
                    return RedirectToAction("Login", "AdminLogin", new { area = "Security" });
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Login failed " + ex;
                return RedirectToAction("Login", "AdminLogin", new { area = "Security" });
            }
        }
        public ActionResult Logout()
        {
            SessionAbend();
            return RedirectToAction("Login", "AdminLogin", new { area = "Security" });
        }
        public void SessionAbend()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
        }
    }
}