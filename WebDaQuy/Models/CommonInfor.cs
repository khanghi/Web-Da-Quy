using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebDaQuy.Models
{
    public class CommonInfo
    {
        private QL_WebDaQuyEntities db;
        public CommonInfo()
        {
            this.db = new QL_WebDaQuyEntities();
        }
        public IEnumerable<ChungLoai> ChungLoais
        {
            get
            {
                return this.db.ChungLoais;
            }
        }
        public IEnumerable<LoaiSP> LoaiSPs
        {
            get
            {
                return this.db.LoaiSPs;
            }
        }
        public IEnumerable<SanPham> SanPhams
        {
            get
            {
                return this.db.SanPhams;
            }
        }
        public IEnumerable<V_soSP> V_soSPs
        {
            get
            {
                return this.db.V_soSP;
            }
        }
        public IEnumerable<V_hotSP> V_hotSPs
        {
            get
            {
                return this.db.V_hotSP;
            }
        }
        public IEnumerable<V_newSP> V_newSPs
        {
            get
            {
                return this.db.V_newSP;
            }
        }
        public IEnumerable<V_tlSP> V_tlSPs
        {
            get
            {
                return this.db.V_tlSP;
            }
        }
        public IEnumerable<V_allSP> V_allSPs
        {
            get
            {
                return this.db.V_allSP;
            }
        }
        public IEnumerable<HinhChiTiet> hinhChiTiets
        {
            get
            {
                return this.db.HinhChiTiets;
            }
        }
        public static List<TaiKhoan> getKhachHang()
        {
            return new DbContext("name=QL_WebDaQuyEntities").Set<TaiKhoan>().ToList<TaiKhoan>();
        }
        public static List<TaiKhoan> getAccount()
        {
            List<TaiKhoan> k = new List<TaiKhoan>();
            DbContext cn = new DbContext("name=QL_WebDaQuyEntities");
            //----Lấy dữ liệu.......
            k = cn.Set<TaiKhoan>().ToList<TaiKhoan>();
            return k;
        }
        public static TaiKhoan getTaiKhoanById(string taiKhoan1)
        {
            return new DbContext("name=QL_WebDaQuyEntities").Set<TaiKhoan>().Find(taiKhoan1);
        }
        public static LoaiSP getLoaiSPById(int maLoai)
        {
            return new DbContext("name=QL_WebDaQuyEntities").Set<LoaiSP>().Find(maLoai);
        }

        ///--- Hàm cho phép lấy ra danh sách sản phẩm theo ID cho CartShop---///
        public static SanPham getProductByID(string maSP)
        {
            return new DbContext("name=QL_WebDaQuyEntities").Set<SanPham>().Find(maSP);
        }

        ///--- Hàm cho phép lấy ra "TÊN" của sản phẩm theo ID cho CartShop---///
        public static string getNameProductByID(string maSP)
        {
            return new DbContext("name=QL_WebDaQuyEntities").Set<SanPham>().Find(maSP).tenSP;
        }

        ///--- Hàm cho phép lấy ra "ẢNH" của sản phẩm theo ID cho CartShop---///
        public static string getImageProductByID(string maSP)
        {
            return new DbContext("name=QL_WebDaQuyEntities").Set<SanPham>().Find(maSP).hinhDD;
        }
    }
}