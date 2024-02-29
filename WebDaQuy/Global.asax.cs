using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebDaQuy.Models;

namespace WebDaQuy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Application["dungChung"] = new CommonInfo();
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["TtDangNhap"] = null;
            ////----Cấp cho người truy cập 1 giỏi hàng chưa chứa gì cả  
            Session["GioHang"] = new CartShop();
        }
    }
}
