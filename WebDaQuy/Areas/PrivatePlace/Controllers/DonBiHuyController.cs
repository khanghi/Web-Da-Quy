using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Areas.PrivatePlace.Controllers
{
    public class DonBiHuyController : Controller
    {
        private static QL_WebDaQuyEntities db = new QL_WebDaQuyEntities();
        private void Update_DaoDien()
        {
            List<DonHang> ldh = db.DonHangs.Where(x => x.daKichHoat == false).ToList<DonHang>();
            ViewData["DanhSachDh"] = ldh;
        }
        // GET: PrivatePlace/DonBiHuy
        [HttpGet]
        public ActionResult Index()
        {
            Update_DaoDien();
            return View();
        }
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
        public ActionResult Delete(string maDonHang)
        {
            //----Xóa bài viết--------\\
            //-b1--khai báo thằng xóa
            int xoa = int.Parse(maDonHang);
            //-b2-- gắn thằng xóa = bv
            DonHang dh = db.DonHangs.Find(xoa);
            //-b2--xóa thằng bv đi
            db.DonHangs.Remove(dh);
            //----Cập nhật vào database
            db.SaveChanges();
            //----Hiển thị lai khi đã xóa
            Update_DaoDien();
            return View("Index");
        }
    }
}