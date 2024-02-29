using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Areas.PrivatePlace.Controllers
{
    public class DangKinhDoanhController : Controller
    {
        private static QL_WebDaQuyEntities db = new QL_WebDaQuyEntities();
        private static bool daDuyet;
        private static bool isUpdate = false;
        // GET: PrivatePlace/DangKinhDoanh
        private void Update_DaoDien()
        {

            List<SanPham> lsp = db.SanPhams.Where(x => x.daDuyet == daDuyet).OrderBy(x => x.tenSP).ToList<SanPham>();
            ViewData["DanhSachSp"] = lsp;
            ViewBag.TenCuaNut = daDuyet ? "Chặn bài" : "Duyệt bài";
        }
        public ActionResult Index(string IsActive)
        {
            if (IsActive != null)
            {
                daDuyet = IsActive.Equals("1");
                Update_DaoDien();
                return View();
            }
            return View("~/Views/Login/Index.cshtml");
        }
        [HttpPost]
        public ActionResult Delete(string maSanPham)
        {
            //-b1--khai báo thằng xóa
            string xoa = string.Concat(maSanPham);
            //-b2-- gắn thằng xóa = sản phẩm
            SanPham sp = db.SanPhams.Find(xoa);
            //-b2--xóa thằng bv đi
            db.SanPhams.Remove(sp);
            //----Cập nhật vào database
            db.SaveChanges();
            //----Hiển thị lai khi đã xóa
            if (ModelState.IsValid)
                ModelState.Clear();
            Update_DaoDien();
            return View("Index");
        }
        [HttpPost]
        public ActionResult Active(string maSanPham)
        {
            //----Duyệt bài viết
            SanPham bv = db.SanPhams.Find(maSanPham);
            bv.daDuyet = !daDuyet;
            //----Cập nhật vào database
            db.SaveChanges();
            //----Hiển thị lai khi đã xóa
            Update_DaoDien();
            return View("Index");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(HttpPostedFileBase hinhSanPham, SanPham x)
        {

            QL_WebDaQuyEntities db = new QL_WebDaQuyEntities();
            ViewBag.htHinh = "/Asset/Images/cay-thong-noel.jpg";
            if (isUpdate)
            {
                SanPham y = db.SanPhams.Find(x.maSP);
                y.tenSP = x.tenSP;
                y.ndTomTat = x.ndTomTat;
                y.giaBan = x.giaBan;
                y.maLoai = x.maLoai;
                y.dvt = "đồng";
                y.daDuyet = false;
                y.ngayDang = DateTime.Now;
                y.taiKhoan = Login.GetTaiKhoan();
                y.noiDung = x.noiDung;
                if (hinhSanPham != null)
                {
                    //----lưu hình vào thư mục bài viết UwU
                    string virPath = "/Asset/Images/"; //-- đường dẫn ảo đi đến thư mục bài viết chứa ảnh
                    string phyPath = Server.MapPath("~/" + virPath); //- Sever.MapPath chỉ ổ đĩa sever tự chọn + đường dẫn vật lí
                    string moRong = Path.GetExtension(hinhSanPham.FileName); //- phần đuôi của hình (.jpg) or (.png).v.v......
                    string fileName = "hBV" + x.maSP + moRong;
                    //---------lưu dựa vào đường dẫn----------------
                    hinhSanPham.SaveAs(phyPath + fileName); //--lưu dựa vào đường dẫn vật lí sever chứa web
                                                            //--nhận đường dẫn truy cập tới hình đã lưu dữ dựa vào domain
                    x.hinhDD = virPath + fileName; //-đường dẫn ảo theo domain
                                                   //---cập nhật hình vừa đăng lên giao diện
                    ViewBag.htHinh = x.hinhDD;
                }
                else
                {
                }
                isUpdate = false;
            }
            db.SaveChanges(); //-- lưu vào database
                              //----update vao view
            if (ModelState.IsValid)
                ModelState.Clear();
            ViewData["DanhSachSp"] = db.SanPhams.OrderBy(m => m.tenSP).ToList<SanPham>();
            return View("Index");

        }
        [HttpPost]
        public ActionResult Update(string mspcs)
        {
            //--phân tích cú pháp của mã loại cần sửa gán = ma
            string ma = string.Concat(mspcs);
            //----Tìm loại sản phẩm trong data models
            SanPham k = db.SanPhams.Find(ma);
            //--- tìm thấy rồi giờ Chỉnh sửa lại nó đi --- 
            isUpdate = true;
            //----Update dao diện---
            ViewData["DanhSachSp"] = db.SanPhams.OrderBy(m => m.tenSP).ToList<SanPham>();
            return View("Update", k);

        }
    }
}