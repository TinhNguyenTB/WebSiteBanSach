using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: GioHang
        public List<GioHang> GetGioHang()
        {
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            if (gioHang == null)
            {
                gioHang = new List<GioHang>();
                Session["GioHang"] = gioHang;
            }
            return gioHang;
        }
        public ActionResult ThemVaoGioHang(int MaSach, string url)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay ra session gio hang
            List<GioHang> lst = GetGioHang();
            // kiem tra sach da ton tai trong gio hang chua
            GioHang sanpham = lst.Find(n => n.MaSach == MaSach);
            if(sanpham == null)
            {
                sanpham = new GioHang(MaSach);
                // them sach vao gio hang
                lst.Add(sanpham);
                return Redirect(url);
            }
            else
            {
                sanpham.SoLuong++;
                return Redirect(url);
            }
        }
        // Cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int MaSach, FormCollection f)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay ra session gio hang
            List<GioHang> lst = GetGioHang();
            // kiem tra sach da ton tai trong gio hang chua
            GioHang sanpham = lst.SingleOrDefault(n => n.MaSach == MaSach);
            if(sanpham != null)
            {
                sanpham.SoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay ra session gio hang
            List<GioHang> lst = GetGioHang();
            // kiem tra sach da ton tai trong gio hang chua
            GioHang sanpham = lst.SingleOrDefault(n => n.MaSach == MaSach);
            if(sanpham != null)
            {
                lst.RemoveAll(n => n.MaSach == MaSach);  
            }
            if(lst.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lst = GetGioHang();
            ViewBag.TongTien = TongTien();
            return View(lst);
        }
        private int TongSoLuong()
        {
            int TongSL = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                TongSL = lst.Sum(n => n.SoLuong);
            }
            return TongSL;
        }
        private int TongTien()
        {
            int TongTien = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                TongTien = lst.Sum(n => n.ThanhTien);
            }
            return TongTien;
        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSL = TongSoLuong();
             
            return PartialView();
        }

        // Đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            // Kiểm tra đăng nhập
            if(Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            // Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Thêm đơn hàng
                DonHang donHang = new DonHang();
                List<GioHang> giohang = GetGioHang();
                KhachHang khachHang = (KhachHang)Session["TaiKhoan"];
                donHang.MaKH = khachHang.MaKH;
                donHang.NgayDat = DateTime.Now;
                db.DonHangs.Add(donHang);
                db.SaveChanges(); // lưu vào database
           

            // Thêm chi tiết đơn hàng
            foreach (var item in giohang)
            {
                ChiTietDonHang ctDonhang = new ChiTietDonHang();
                ctDonhang.MaDonHang = donHang.MaDonHang;
                ctDonhang.MaSach = item.MaSach;
                ctDonhang.SoLuong = item.SoLuong;
                ctDonhang.DonGia = item.DonGia;
                db.ChiTietDonHangs.Add(ctDonhang);
            }
            db.SaveChanges();
            Session.Remove("GioHang");
            return RedirectToAction("Index", "Home");
        }
    }
}