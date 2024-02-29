using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Areas.PrivatePlace.Controllers
{
    public class DangGiaoController : Controller
    {
        private static QL_WebDaQuyEntities db = new QL_WebDaQuyEntities();
        // GET: PrivatePlace/DangGiao
        private void Update_DaoDien()
        {
            List<DonHang> ldh = db.DonHangs.Where(x => x.daKichHoat == true).ToList<DonHang>();
            ViewData["DanhSachDh"] = ldh;
        }
        [HttpGet]
        public ActionResult Index()
        {
            Update_DaoDien();
            return View();
        }
        [HttpPost]
        public ActionResult huyDon(string maDonHang)
        {
            //----Duyệt bài viết
            DonHang dh = db.DonHangs.Find(maDonHang);
            dh.daKichHoat = false;

            //----Cập nhật vào database
            db.SaveChanges();
            //----Hiển thị lai khi đã xóa
            Update_DaoDien();
            return View("Index");
        }
        [HttpPost]
        public ActionResult thanhCong(string maDonHang)
        {
            //----Duyệt bài viết
            DonHang dh = db.DonHangs.Find(maDonHang);
            dh.daKichHoat = true;

            //----Cập nhật vào database
            db.SaveChanges();
            //----Hiển thị lai khi đã xóa
            Update_DaoDien();
            return View("Index");
        }
    }
}