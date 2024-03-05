using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;
using System.Data.Entity;

namespace WebSiteBanSach.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            // Kiểm tra đăng nhập
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10; // số đơn hàng trên 1 trang
            return View(db.DonHangs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(pageNumber, pageSize));
        }
        // sửa
        [HttpGet]
        public ActionResult Sua(int MaDonHang)
        {
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(donhang);
        }
        [HttpPost]
        public ActionResult Sua(DonHang donhang)
        {

            db.Entry(donhang).State = EntityState.Modified;

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // Xem chi tiet
        public ActionResult XemChiTiet(int MaDonHang)
        {
            // Lấy đơn hàng từ mã đơn hàng
            DonHang donHang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);

            // Lấy danh sách chi tiết đơn hàng tương ứng với mã đơn hàng
            List<ChiTietDonHang> chiTietDonHangs = db.ChiTietDonHangs.Where(n => n.MaDonHang == MaDonHang).ToList();

            // Tạo một đối tượng DonHangViewModel để chứa đơn hàng và danh sách chi tiết đơn hàng
            DonHangViewModel viewModel = new DonHangViewModel
            {
                DonHang = donHang,
                ChiTietDonHangs = chiTietDonHangs
            };

            return View(viewModel);
        }
        // Xóa
        [HttpGet]
        public ActionResult Xoa(int MaDonHang)
        {
            DonHang donHang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);
            if (donHang == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(donHang);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaDonHang)
        {
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ChiTietDonHangs.RemoveRange(db.ChiTietDonHangs.Where(x => x.MaDonHang == MaDonHang));
            db.DonHangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}