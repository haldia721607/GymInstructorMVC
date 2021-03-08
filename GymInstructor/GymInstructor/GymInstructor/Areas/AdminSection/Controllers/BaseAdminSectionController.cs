using GymInstructor.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace GymInstructor.Areas.AdminSection.Controllers
{
    [Authorize]
    [SessionExpire]
    public class BaseAdminSectionController : Controller
    {
        // GET: AdminSection/BaseAdminSection
        public AdminArearBs adminArearBs;
        public BaseAdminSectionController()
        {
            adminArearBs = new AdminArearBs();
        }
    }
}