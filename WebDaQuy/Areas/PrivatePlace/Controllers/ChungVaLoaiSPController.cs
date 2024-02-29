using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Areas.PrivatePlace.Controllers
{
    public class ChungVaLoaiSPController : Controller
    {
        private static bool isUpdate = false;
        // GET: PrivatePlace/ChungVaLoaiSP
        [HttpGet]
        public ActionResult Index()
        {
            List<LoaiSP> l = new QL_WebDaQuyEntities().LoaiSPs.OrderBy(x => x.tenLoai).ToList<LoaiSP>();
            ViewData["DsLoai"] = l;
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoaiSP x)
        {
            QL_WebDaQuyEntities sh = new QL_WebDaQuyEntities();
            if (!isUpdate)
            {
                sh.LoaiSPs.Add(x); //--- thêm vào
            }
            else
            {
                LoaiSP lsp = sh.LoaiSPs.Find(x.maLoai);
                lsp.tenLoai = x.tenLoai;
                lsp.ghiChu = x.ghiChu;
                isUpdate = false;
            }
            sh.SaveChanges();  //--- lưu lại vào database 
            //-- update chuỗi vào view
            if (ModelState.IsValid)
                ModelState.Clear();
            ViewData["DsLoai"] = sh.LoaiSPs.OrderBy(z => z.tenLoai).ToList<LoaiSP>();
            return View();

        }
        [HttpPost]
        public ActionResult Delete(string ml)
        {
            QL_WebDaQuyEntities sh = new QL_WebDaQuyEntities();
            int ma = int.Parse(ml);
            //----Tìm loại sản phẩm trong data models
            LoaiSP lsp = sh.LoaiSPs.Find(ma);
            //--- tìm thấy rồi giờ xóa nó đi ---
            sh.LoaiSPs.Remove(lsp);
            //--- cập nhật cho database
            sh.SaveChanges();
            //-------------------------------//
            ViewData["DsLoai"] = sh.LoaiSPs.OrderBy(z => z.tenLoai).ToList<LoaiSP>();
            return View("Index");
        }
        [HttpPost]
        public ActionResult Update(string mlcs)
        {
            QL_WebDaQuyEntities sh = new QL_WebDaQuyEntities();
            int ma = int.Parse(mlcs);
            //----Tìm loại sản phẩm trong data models
            LoaiSP lsp = sh.LoaiSPs.Find(ma);
            //--- tìm thấy rồi giờ Chỉnh sửa lại nó đi --- 
            isUpdate = true;
            //sh.SaveChanges();
            //-------------------------------//
            ViewData["DsLoai"] = sh.LoaiSPs.OrderBy(z => z.tenLoai).ToList<LoaiSP>();
            return View("Index", lsp);
        }
    }
}