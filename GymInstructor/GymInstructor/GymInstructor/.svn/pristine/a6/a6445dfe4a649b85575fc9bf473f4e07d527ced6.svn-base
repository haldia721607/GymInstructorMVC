using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymInstructor.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class VerifyController : BaseSecurityController
    {
        // GET: Security/Verify
        public ActionResult VerifyEmail(string Id)
        {
            try
            {
                string userId = CommanFunction.Decrypt(HttpUtility.UrlDecode(Request.QueryString["Id"]));
                var getUserActivationStatus = securityAreaBs.userBs.GetUserIsActiveOrNot(userId);
                if (getUserActivationStatus == true)
                {
                    TempData["Msg"] = "You have already verified your account. Go to login page for login your account.";
                }
                else
                {
                    var updateUserIsActive = securityAreaBs.userBs.UpdateUserIsActive(userId);
                    if (updateUserIsActive == true)
                    {
                        TempData["Msg"] = "You have successfully verified your account. Go to login page for login your account.";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}