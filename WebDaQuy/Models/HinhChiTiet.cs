//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDaQuy.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HinhChiTiet
    {
        public int STT { get; set; }
        public string maSP { get; set; }
        public string hinhCT { get; set; }
    
        public virtual SanPham SanPham { get; set; }
    }
}
