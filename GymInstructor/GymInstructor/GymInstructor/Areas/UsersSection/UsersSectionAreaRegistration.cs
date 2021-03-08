using System.Web.Mvc;

namespace GymInstructor.Areas.UsersSection
{
    public class UsersSectionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UsersSection";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UsersSection_default",
                "UsersSection/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}