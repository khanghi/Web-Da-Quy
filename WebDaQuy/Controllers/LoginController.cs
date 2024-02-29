using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Acc, string Pass)
        {
            //--- Đọc tài khoản từ Database ---//
            TaiKhoan ttdn = new QL_WebDaQuyEntities().TaiKhoans.Where(x => x.taiKhoan1.Equals(Acc.ToLower().Trim())
                          && x.matKhau.Equals(Pass)).First<TaiKhoan>();
            //--- Hàm kiểm tra tài khoản ---//
            bool isAuthentic = (ttdn != null) && ttdn.taiKhoan1.Equals(Acc.ToLower().Trim()) && ttdn.matKhau.Equals(Pass);
            if (isAuthentic && ttdn.ChucNang == 1)
            {
                Session["TtDangNhap"] = ttdn;
                return RedirectToAction("Index", "Dashboard", new { area = "PrivatePlace" });
            }
            else if (isAuthentic)
            {
                Session["TtDangNhap"] = ttdn;
                return View("~/Views/Home/Index.cshtml");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection col, TaiKhoan tk)
        {
            QL_WebDaQuyEntities db = new QL_WebDaQuyEntities();
            var tendn = col["taiKhoan1"];
            var mk = col["matKhau"];
            var e = col["email"];
            var dc = col["diaChi"];
            var sdt = col["soDT"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = " Tên đăng nhập là thông tin bắt buộc  ";
            }
            if (CommonInfo.getTaiKhoanById(tendn) != null)
            {
                ViewData["loi6"] = " Tên đăng nhập bị trùng  ";
            }
            if (string.IsNullOrEmpty(mk))
            {
                ViewData["loi2"] = " Mật khẩu là thông tin bắt buộc ";
            }
            if (string.IsNullOrEmpty(e))
            {
                ViewData["loi3"] = " Email là thông tin bắt buộc ";
            }
            if (string.IsNullOrEmpty(dc))
            {
                ViewData["loi4"] = " Địa chỉ là thông tin bắt buộc ";
            }
            if (string.IsNullOrEmpty(sdt))
            {
                ViewData["loi5"] = " Số điện thoại là thông tin bắt buộc ";
            }
            else
            {

                tk.taiKhoan1 = tendn;
                tk.matKhau = mk;
                tk.email = e;
                tk.diaChi = dc;
                tk.soDT = sdt;
                db.TaiKhoans.Add(tk);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/Home/Index.cshtml");
        }
        public ActionResult Logout()
        {
            Session["TtDangNhap"] = null;//remove session
            Session["GioHang"] = new CartShop();
            return RedirectToAction("Index", "Home");
        }
    }
}