using System.Web.Mvc;

namespace WebDaQuy.Areas.PrivatePlace
{
    public class PrivatePlaceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PrivatePlace";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PrivatePlace_default",
                "PrivatePlace/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}