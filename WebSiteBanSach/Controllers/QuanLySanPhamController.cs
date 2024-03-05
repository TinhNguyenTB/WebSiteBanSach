using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Globalization;
using System.Data.Entity;

namespace WebSiteBanSach.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int ? page) //int? có nghĩa là nó có thể chấp nhận một giá trị số nguyên hoặc có thể bị thiếu (null)
        {
            // Kiểm tra đăng nhập
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            // Nếu tham số page được truyền vào với một giá trị số nguyên, thì pageNumber sẽ lấy giá trị của page
            // Nếu tham số page không được truyền hoặc là null, thì pageNumber sẽ được gán giá trị mặc định là 1
            int pageNumber = (page ?? 1);  
            int pageSize = 9; // số sản phẩm trên 1 trang
            return View(db.Saches.ToList().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
        }

        // Thêm mới
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaChuDe = new SelectList( db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList( db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileUpload)
        {
            // Load dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            // kiểm tra đường dẫn ảnh bìa
            if(fileUpload == null)
            {
                ViewBag.message = "Vui lòng thêm hình ảnh!";
                return View();
            }

            // Thêm vào database
            if (ModelState.IsValid)
            {
                // Lưu tên của file
                var fileName = Path.GetFileName(fileUpload.FileName);
                // Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP/"), fileName);
                // Kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.message = "Hình ảnh đã tồn tại!";
                }
                // nếu chưa tồn tại thì lưu hình ảnh vào thư mục 
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.AnhBia = fileUpload.FileName;
                db.Saches.Add(sach);
                db.SaveChanges();
            }
            return View();
        }

        // sửa
        [HttpGet]
        public ActionResult Sua(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }
        [HttpPost]
        public ActionResult Sua(Sach sach, HttpPostedFileBase fileAnhBia)
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);

            db.Entry(sach).State = EntityState.Modified;

            if (fileAnhBia != null && fileAnhBia.ContentLength > 0)
            {
                string fileName = Path.GetFileName(fileAnhBia.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP/"), fileName);
                fileAnhBia.SaveAs(path);
                sach.AnhBia = fileName;
            }

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index"); 
            }
            return View();
        }
        // Xem chi tiet
        public ActionResult XemChiTiet(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        // Xóa
        [HttpGet]
        public ActionResult Xoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(sach);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ChiTietDonHangs.RemoveRange(db.ChiTietDonHangs.Where(x => x.MaSach == MaSach));
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}