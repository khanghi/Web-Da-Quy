using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using WebDaQuy.Models;


namespace WebDaQuy.Areas.PrivatePlace.Controllers
{
    public class SPMoiController : Controller
    {
        public void DangSanPham()
        {
            List<SanPham> sp = new QL_WebDaQuyEntities().SanPhams.OrderBy(x => x.tenSP).ToList<SanPham>();
            ViewData["DangSp"] = sp;
        }
        // GET: PrivatePlace/SPMoi
        [HttpGet]
        public ActionResult Index()
        {
            //---khai báo
            QL_WebDaQuyEntities sh = new QL_WebDaQuyEntities();
            SanPham x = new SanPham();
            x.maSP = string.Format("{0:ddMMyyhhmm}", DateTime.Now);
            //--các loại = mặc định
            x.ngayDang = DateTime.Now;
            x.daDuyet = false;
            x.dvt = "Cái";
            x.taiKhoan = Login.GetTaiKhoan();
            ViewBag.htHinh = "~/Images/cay-thong.jpg";
            x.nhaSanXuat = "";
            x.giamGia = 0;
            DangSanPham();
            return View(x);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(SanPham e, HttpPostedFileBase hinhSanPham)
        {
            try
            {
                QL_WebDaQuyEntities sh = new QL_WebDaQuyEntities();
                e.daDuyet = false;
                e.ngayDang = DateTime.Now;
                e.taiKhoan = Login.GetTaiKhoan();
                e.maSP = string.Format("{0:ddMMyyhh}", DateTime.Now);
                e.giamGia = 0;
                e.nhaSanXuat = "";
                e.dvt = "Cái";
                if (hinhSanPham != null)
                {
                    //----lưu hình vào thư mục bài viết UwU
                    string virPath = "~/Images/"; //-- đường dẫn ảo đi đến thư mục bài viết chứa ảnh
                    string phyPath = Server.MapPath("~/" + virPath); //- Sever.MapPath chỉ ổ đĩa sever tự chọn + đường dẫn vật lí
                    string moRong = Path.GetExtension(hinhSanPham.FileName); //- phần đuôi của hình (.jpg) or (.png).v.v......
                    string fileName = "hBV" + e.maSP + moRong;
                    //---------lưu dựa vào đường dẫn----------------
                    hinhSanPham.SaveAs(phyPath + fileName); //--lưu dựa vào đường dẫn vật lí sever chứa web
                                                            //--nhận đường dẫn truy cập tới hình đã lưu dữ dựa vào domain
                    e.hinhDD = virPath + fileName; //-đường dẫn ảo theo domain
                                                   //---cập nhật hình vừa đăng lên giao diện
                    ViewBag.htHinh = e.hinhDD;
                }
                else { e.hinhDD = ""; }
                //-------thêm dữ liệu 
                sh.SanPhams.Add(e);
                //---lưu dữ liệu vừa thêm vào dataabase
                sh.SaveChanges();
                //--- nếu đăng bài thành công thì chuyển đến trang duyệt bài 
                return View("~/Areas/PrivatePlace/Views/SPMoi/Index.cshtml");
            }
            catch
            {
                //---nếu không đăng thì 
                return View(e);
            }
        }
    }
}