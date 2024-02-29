using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDaQuy.Models
{
    public class CartShop
    {
        public string MaKH { get; set; }

        public string TaiKhoan { get; set; }
        public DateTime NgayDat { get; set; }
        public DateTime NgayGiao { get; set; }
        public string DiaChi { get; set; }
        ////-----
        ///----List ; ArrayList ; SortedList;..... <key> - <Value>
        public SortedList<string, CtDonHang> SanPhamDC { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public CartShop()
        {
            this.MaKH = ""; this.TaiKhoan = ""; this.NgayDat = DateTime.Now; this.NgayGiao = DateTime.Now.AddDays(2);
            this.DiaChi = "";
            this.SanPhamDC = new SortedList<string, CtDonHang>();
        }
        /// <summary>
        /// Phương thức trả về true nếu không có sản phẩm nào mua trong hệ thống 
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return SanPhamDC.Keys.Count == 0;
        }
        /// <summary>
        /// phương thức thêm 1 sản phẩm đã chọn mua vào giỏ hàng có 2 tình huống 
        /// </summary>
        /// <param name="maSP"></param>


        ///----Viết thêm 1 hàm dựa vào việc xóa cái cũ và thêm cái mới 

        public void updateOne(CtDonHang x)
        {
            this.SanPhamDC.Remove(x.maSP);
            this.SanPhamDC.Add(x.maSP, x);
        }

        public void addItem(string maSP)
        {
            if (SanPhamDC.Keys.Contains(maSP))
            {
                ///--- LẤY SẢN PHẨM TỪ TRONG GIỎ HÀNG 
                CtDonHang x = SanPhamDC.Values[SanPhamDC.IndexOfKey(maSP)];
                ///--- TĂNG SỐ LƯỢNG LÊN 1 
                x.soLuong++;
                ///---- NHÉT TRỞ LẠI GIỎ HÀNG SAU KHI ĐÃ CẬP NHẬT SỐ LƯỢNG 
                updateOne(x);
            }
            else
            {
                ///----- Tạo 1 object  chi tiết đơn hàng mới  
                CtDonHang i = new CtDonHang();
                ///---- Cập nhật thông tin hiện hành từ hệ thống cho đối tượng 
                i.maSP = maSP;
                i.soLuong = 1;
                ///---- lấy giá bán ; lấy giảm giá  từ table SanPham
                SanPham z = CommonInfo.getProductByID(maSP);
                i.giaBan = z.giaBan;
                i.giamGia = z.giamGia;
                ///---- bỏ vào danh sách các sản phẩm đã chọn mua trong giỏ hàng của mình 
                SanPhamDC.Add(maSP, i);
            }

        }
        /// <summary>
        /// xóa 1 sản phẩm trong giỏ hàng 
        /// </summary>
        /// <param name="maSP"></param>
        public void deleteItem(string maSP)
        {
            if (SanPhamDC.Keys.Contains(maSP))
            {
                SanPhamDC.Remove(maSP);
            }

        }
        /// <summary>
        /// cho phép giảm số lượng hoặc xóa sản phẩm đã chọn hoặc danh sách giỏ hàng 
        /// </summary>
        /// <param name="maSP"></param>
        public void decrease(string maSP)
        {
            if (SanPhamDC.Keys.Contains(maSP))
            {
                CtDonHang x = SanPhamDC.Values[SanPhamDC.IndexOfKey(maSP)];
                if (x.soLuong > 1)
                {
                    x.soLuong--;

                    updateOne(x);
                }
                else
                    deleteItem(maSP);
            }

        }
        /// <summary>
        /// Tính trị giá tiền của 
        /// </summary>
        /// <param name="maSP"></param>
        /// <returns></returns>
        public long moneyOfOneProduct(CtDonHang x)
        {
            return (long)(x.giaBan * x.soLuong - (x.giaBan * x.soLuong * x.giamGia));

        }
        /// <summary>
        /// tính tổng thành viên cho toàn bộ giỏ hàng 
        /// </summary>
        /// <returns></returns>
        public long totalOfCartShop()
        {
            long kq = 0;

            foreach (CtDonHang i in SanPhamDC.Values)
                kq += moneyOfOneProduct(i);
            return kq;
        }
    }
}