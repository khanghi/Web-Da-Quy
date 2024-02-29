using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Controllers
{
    public class CheckOutSuccessController : Controller
    {
        // GET: CheckOutSuccess
        public ActionResult Index()
        {
            //--Lấy giỏ hàng từ session hiện thị trên checkoutsuccess
            CartShop gh = Session["GioHang"] as CartShop;
            ViewData["GioHang"] = gh;
            //--xóa giỏ hàng trong session
            Session["GioHang"] = new CartShop();
            return View();
        }
    }
}