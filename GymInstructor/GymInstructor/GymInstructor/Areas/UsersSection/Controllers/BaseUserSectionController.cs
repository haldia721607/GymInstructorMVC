using BLL;
using BOL;
using GymInstructor.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymInstructor.Areas.UsersSection.Controllers
{
    [Authorize]
    [SessionExpire]
    public class BaseUserSectionController : Controller
    {
        public UserBs userBs;
        // GET: UsersSection/BaseUserSection
        public BaseUserSectionController()
        {
            userBs = new UserBs();
        }
    }
}