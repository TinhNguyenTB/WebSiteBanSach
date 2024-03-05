using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;

namespace WebSiteBanSach.Controllers
{
    public class QuanLyKhachHangController : Controller
    {
        // GET: QuanLyKhachHang
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page) //int? có nghĩa là nó có thể chấp nhận một giá trị số nguyên hoặc có thể bị thiếu (null)
        {
            // Kiểm tra đăng nhập
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            // Nếu tham số page được truyền vào với một giá trị số nguyên, thì pageNumber sẽ lấy giá trị của page
            // Nếu tham số page không được truyền hoặc là null, thì pageNumber sẽ được gán giá trị mặc định là 1
            int pageNumber = (page ?? 1);
            int pageSize = 10; // số khach hang trên 1 trang
            return View(db.KhachHangs.ToList().OrderBy(n => n.MaKH).ToPagedList(pageNumber, pageSize));
        }
        // Xem chi tiet
        public ActionResult XemChiTiet(int MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        // Xóa
        [HttpGet]
        public ActionResult Xoa(int MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Lấy danh sách đơn hàng của khách hàng
            List<DonHang> donHangs = db.DonHangs.Where(x => x.MaKH == MaKH).ToList();

            // Duyệt qua từng đơn hàng và xóa liên quan
            foreach (var donHang in donHangs)
            {
                // Xóa tất cả chi tiết đơn hàng của đơn hàng
                db.ChiTietDonHangs.RemoveRange(db.ChiTietDonHangs.Where(x => x.MaDonHang == donHang.MaDonHang));

                // Xóa đơn hàng
                db.DonHangs.Remove(donHang);
            }

            // Xóa khách hàng
            db.KhachHangs.Remove(kh);

            // Lưu thay đổi vào CSDL
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}