using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDaQuy.Models;

namespace WebDaQuy.Controllers
{
    public class detailSPController : Controller
    {
        QL_WebDaQuyEntities obj = new QL_WebDaQuyEntities();
        // GET: detailSP
        public ActionResult Index(string IDSP)
        {
            var SP = (from x in obj.SanPhams
                      where x.maSP == IDSP
                      select x).First();
            ViewBag.SP = SP;
            var Hinh = (from c in obj.HinhChiTiets where c.maSP == IDSP select c).ToList();
            ViewBag.Hinh = Hinh;
            ViewBag.Title = SP.tenSP;
            return View();
        }
    }
}