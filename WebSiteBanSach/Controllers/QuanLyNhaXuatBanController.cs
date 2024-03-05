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
    public class QuanLyNhaXuatBanController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: QuanLyNhaXuatBan
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
            int pageSize = 5; // số chủ đề trên 1 trang
            return View(db.NhaXuatBans.ToList().OrderBy(n => n.MaNXB).ToPagedList(pageNumber, pageSize));
        }
        // Thêm mới
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(NhaXuatBan nxb)
        {
            // Thêm vào database
            if (ModelState.IsValid)
            {
                db.NhaXuatBans.Add(nxb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // sửa
        [HttpGet]
        public ActionResult Sua(int MaNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        [HttpPost]
        public ActionResult Sua(NhaXuatBan nxb)
        {
            db.Entry(nxb).State = EntityState.Modified;

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // Xóa
        [HttpGet]
        public ActionResult Xoa(int MaNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(nxb);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Kiểm tra xem nhà xuất bản có sách hay không
            if (db.Saches.Any(x => x.MaNXB == MaNXB))
            {
                // Nếu có sách thuộc nhà xuất bản, thì hiển thị thông báo
                ViewBag.Message = "Nhà xuất bản này đang có sách, bạn không thể xóa!";
                return View(nxb);
            }

            // Nếu không có sách thuộc nhà xuất bản, xóa nxb
            db.NhaXuatBans.Remove(nxb);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}