using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Areas.PrivatePlace.Controllers
{
    public class DangXuLyController : Controller
    {
        private static QL_WebDaQuyEntities db = new QL_WebDaQuyEntities();
        private void Update_DaoDien()
        {
            List<DonHang> ldh = db.DonHangs.Where(x => x.daKichHoat == null).ToList<DonHang>();
            ViewData["DanhSachDh"] = ldh;
        }
        // GET: PrivatePlace/DangXuLy
        public ActionResult Index(string IsActive)
        {
            Update_DaoDien();
            return View("Index");
        }
        /// <summary>
        /// cập nhật dữ liệu cho View controller cho viewdata object..........
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult kichHoat(string maDonHang)
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
    }
}