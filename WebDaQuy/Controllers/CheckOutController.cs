using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;
using System.Data.Entity;

namespace WebDaQuy.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        [HttpGet]
        public ActionResult Index()
        {
            //--tạo đối tượng khách hàng với thông tin mới truyền vào ....
            KhachHang x = new KhachHang();
            //--Lấy thông tin đã mua từ Session Giỏ Hàng truyền cho view thông qua ViewData
            CartShop gh = Session["GioHang"] as CartShop;
            //--Truyền ra View
            ViewData["Cart"] = gh;
            Session["TtKhachHang"] = x;
            return View(x);
        }
        [HttpPost]
        public ActionResult Index(KhachHang x)
        {
            //--ứng dụng để lưu đồng thời các table khác nhau
            using (var context = new QL_WebDaQuyEntities())
            {
                using (DbContextTransaction trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        //--1-> tạo một đối tượng khách hàng -------------------bảng KhachHang
                        //--2-> cập nhật đối tượng khách hàng đã tạo trước đó
                        x.maKH = x.soDT;
                        x.gioiTinh = x.gioiTinh;
                        //--3-> thêm thông tin khách hàng vào models
                        context.Set<KhachHang>().Add(x);
                        //--4-> lưu lại
                        context.SaveChanges();
                        //--2.1-> Tạo một đối tượng đơn hàng ---------bảng DonHang
                        DonHang d = new DonHang();
                        //--2.2-> cập nhật dữ liệu cho đơn hàng
                        d.soDH = String.Format("{0:yyMMddhhmm}", DateTime.Now);
                        d.maKH = x.maKH;
                        d.ngayDat = DateTime.Now; d.ngayGH = DateTime.Now.AddDays(2);
                        d.taiKhoan = "admin";
                        d.diaChiGH = x.diaChi;
                        //--2.3-> thêm thông tin vào model
                        context.DonHangs.Add(d);
                        //--2.4-> lưu lại
                        context.SaveChanges();
                        //--3.1-> lấy chuỗi các sản phẩm trong cartshop -----------bảng ctDonHang
                        CartShop gh = Session["GioHang"] as CartShop;
                        //--3.2-> cập nhật thông tin DonHang những đối tượng đã tạo trước 
                        foreach (CtDonHang i in gh.SanPhamDC.Values)
                        {
                            i.soDH = d.soDH;
                            context.CtDonHangs.Add(i);
                        }
                        //--3.3-> lưu lại 
                        context.SaveChanges();
                        //----4-> Hoàn thành 
                        trans.Commit();
                        //---chuyển trng thông báo đặt thành công về home --- hoặc làm 1 trang báo đã thành công
                        return RedirectToAction("Index", "CheckOutSuccess");

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                    }
                }
            }
            //--nếu lỗi sẽ truyền về checkout ./..
            return RedirectToAction("Index", "Checkout");
        }
    }
}