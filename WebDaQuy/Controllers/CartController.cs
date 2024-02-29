using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [HttpGet]
        public ActionResult Index()
        {
            //--- Tạo 1 đối tượng khách hàng với thông tin mới truyền ra cho view ---//
            KhachHang x = new KhachHang();
            //--- Lấy thông tin đã mua hàng từ Session và truyền cho View thông qua ViewData ---//
            //--- Lấy giỏ hàng từ Session ---//
            CartShop gh = Session["GioHang"] as CartShop;
            //--- Truyền ra cho ngoài view ---//
            ViewData["Cart"] = gh;
            return View();
        }

        [HttpPost]
        public ActionResult SaveToDataBase(KhachHang x)
        {
            //--- Sử dụng Transaction để lưu đồng thời dữ liệu trên 3 table khác nhau ---//
            using (var context = new QL_WebDaQuyEntities())
            {
                using (DbContextTransaction trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        //---------------------------------[Table KhachHang]---------------------------------//
                        //--- 1.1/- New a customer object and add to KhachHang domain ----[Table KhachHang]----
                        //--- 1.2/- Update customer info to KhachHang object you have just created before -----
                        x.maKH = string.Format("KH{0:MMddhhmm}", DateTime.Now);
                        //--- 1.3/- Add customer info to Data model--------------------------------------------
                        context.KhachHangs.Add(x);
                        //--- 1.4/- Save to database ----[Table KhachHang]-------------------------------------
                        context.SaveChanges();



                        //----------------------------------[Table DonHang]----------------------------------//
                        //--- 2.1/- New a customer object and add to KhachHang domain ----[Table DonHang]------
                        DonHang d = new DonHang();
                        //--- 2.2/- Update customer info to KhachHang object you have just created before -----
                        d.soDH = string.Format("DH{0:MMddhhmm}", DateTime.Now);
                        d.maKH = x.maKH;
                        d.ngayDat = DateTime.Now; d.ngayGH = DateTime.Now.AddDays(2);
                        d.daKichHoat = false;
                        d.ghiChu = x.ghiChu;
                        d.taiKhoan = "admin"; d.diaChiGH = x.diaChi;
                        //--- 2.3/- Add customer info to Data model--------------------------------------------
                        context.DonHangs.Add(d);
                        //--- 2.4/- Save to database ----[Table DonHang]---------------------------------------
                        context.SaveChanges();



                        //---------------------------------[Table CtDonHang]---------------------------------//
                        //--- 3.1/- Get list of Item from CartShop ---------------------[Table CtDonHang]------
                        CartShop gh = Session["GioHang"] as CartShop;
                        //--- 3.2/- Update customer info to KhachHang object you have just created before -----
                        foreach (CtDonHang i in gh.SanPhamDC.Values)
                        {
                            i.soDH = d.soDH;
                            context.CtDonHangs.Add(i);
                        }
                        //--- 3.3/- Add customer info to Data model--------------------------------------------
                        //--- 3.4/- Save to database ----[Table CtDonHang]-------------------------------------
                        context.SaveChanges();



                        //--------------------------------------[FINISH]-------------------------------------//
                        //--- 4/- Finish and commit all of action above ---------------------------------------
                        trans.Commit();
                        //--- Chuyển đến trang thông báo thành công khi thanh toán ---//
                        return RedirectToAction("Index", "CheckOutSuccess");
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        string s = e.Message;
                    }
                }
            }
            //--- Nếu bị lỗi sẽ chuyển về trang CheckOut ---//
            return RedirectToAction("Index", "CheckOut");
        }
        public ActionResult Increase(string maSP)
        {
            CartShop gh = Session["GioHang"] as CartShop;
            gh.addItem(maSP);
            Session["GioHang"] = gh;
            return RedirectToAction("Index");
        }
        public ActionResult Decrease(string maSP)
        {
            CartShop gh = Session["GioHang"] as CartShop;
            gh.decrease(maSP);
            Session["GioHang"] = gh;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string maSP)
        {
            CartShop gh = Session["GioHang"] as CartShop;
            gh.deleteItem(maSP);
            Session["GioHang"] = gh;
            return RedirectToAction("Index");
        }
    }
}