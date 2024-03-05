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
    public class QuanLyChuDeController : Controller
    {
        // GET: QuanLyChuDe

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
            int pageSize = 5; // số chủ đề trên 1 trang
            return View(db.ChuDes.ToList().OrderBy(n => n.MaChuDe).ToPagedList(pageNumber, pageSize));
        }
        // Thêm mới
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(ChuDe chude)
        {
            // Thêm vào database
            if (ModelState.IsValid)
            {
                db.ChuDes.Add(chude);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // sửa
        [HttpGet]
        public ActionResult Sua(int MaChuDe)
        {
            ChuDe chude = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        [HttpPost]
        public ActionResult Sua(ChuDe chude)
        {
            db.Entry(chude).State = EntityState.Modified;

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        // Xóa
        [HttpGet]
        public ActionResult Xoa(int MaChuDe)
        {
            ChuDe chude = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(chude);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaChuDe)
        {
            ChuDe chude = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Kiểm tra xem chủ đề có sách hay không
            if (db.Saches.Any(x => x.MaChuDe == MaChuDe))
            {
                // Nếu có sách thuộc chủ đề, thì hiển thị thông báo
                ViewBag.Message = "Chủ đề này đang có sách, bạn không thể xóa!";
                return View(chude); 
            }

            // Nếu không có sách thuộc chủ đề, xóa chủ đề
            db.ChuDes.Remove(chude);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}