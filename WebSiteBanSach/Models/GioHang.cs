using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebSiteBanSach.Models
{
    public class GioHang
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string HinhAnh { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien => SoLuong * DonGia;
        public GioHang(int maSach)
        {
            Sach sach = db.Saches.Single(n => n.MaSach == maSach);

            MaSach = maSach;
            TenSach = sach.TenSach;
            HinhAnh = sach.AnhBia;
            DonGia = int.Parse(sach.GiaBan.ToString());
            SoLuong = 1;
        }
    }
}