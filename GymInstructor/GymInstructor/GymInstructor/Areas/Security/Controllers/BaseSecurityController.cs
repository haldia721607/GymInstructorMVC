using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using GymInstructor.App_Start;

namespace GymInstructor.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class BaseSecurityController : Controller
    {
       public SecurityAreaBs securityAreaBs;

        // GET: Security/BaseSecurity
        public BaseSecurityController()
        {
            securityAreaBs = new SecurityAreaBs();
        }
    }
}