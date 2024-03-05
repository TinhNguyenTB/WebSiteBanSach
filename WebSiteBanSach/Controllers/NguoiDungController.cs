using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
       
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                // insert data
                db.KhachHangs.Add(kh);
                // save data
                
                db.SaveChanges();
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            ViewBag.message = "Đăng ký không thành công";
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
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
            if(kh != null)
            {
                //Đăng nhập thành công
                Session["TaiKhoan"] = kh;
                // Chuyển hướng về trang chủ 
                return RedirectToAction("Index", "Home");
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

            // Chuyển hướng về trang chủ 
            return RedirectToAction("Index", "Home");
        }
        
    }
}