using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class AdminController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: Admin
        public ActionResult Index()
        {
            // Kiểm tra đăng nhập
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string taikhoan = f.Get("txtTaiKhoan").ToString();
            string matkhau = f["txtMatKhau"].ToString();
            Admin admin = db.Admins.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
            if (admin != null)
            {
                Session["Admin"] = admin;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.message = "Tài khoản hoặc mật khẩu không chính xác";
            return View();
        }
        public ActionResult DangXuat()
        {
            // Đăng xuất khỏi hệ thống
            FormsAuthentication.SignOut();

            // Xóa Session để đảm bảo đăng xuất hoàn toàn
            Session.Clear();

            // Chuyển hướng về trang đăng nhập
            return RedirectToAction("DangNhap", "Admin");
        }
    }
}