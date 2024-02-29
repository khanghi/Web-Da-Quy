using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Controllers
{
    public class allSPController : Controller
    {
        QL_WebDaQuyEntities obj = new QL_WebDaQuyEntities();
        // GET: allSP
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductCatogery(int ID)
        {
            var listSP = obj.SanPhams.Where(n => n.maLoai == ID).ToList();
            ViewBag.SanPhams = listSP;
            var tl = (from x in obj.LoaiSPs where x.maLoai == ID select x.tenLoai).First();
            ViewBag.Title = tl;
            return View();
        }
        public ActionResult Catogery(int IDChung)
        {
            List<SanPham> ds = (from x in obj.SanPhams
                                join l in obj.LoaiSPs on x.maLoai equals l.maLoai
                                where l.maChung == IDChung
                                select x).ToList();
            ViewBag.Loais = ds;
            var tl = (from c in obj.ChungLoais where c.maChung == IDChung select c.tenChung).First();
            ViewBag.Title = tl;
            return View();
        }
    }
}