using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDaQuy.Models;

namespace WebDaQuy.Models
{
    public class Login
    {
        public static TaiKhoan TkHienHanh()
        {
            TaiKhoan x = HttpContext.Current.Session["TtDangNhap"] as TaiKhoan;
            return x;
        }
        public static string GetTaiKhoan()
        {
            string tenTK = "";
            try
            {
                tenTK = TkHienHanh().taiKhoan1;
            }
            catch
            {

            }
            return tenTK;
        }
    }
}