using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View(db.Saches.Take(3).ToList());
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult SanPham()
        {
            var lstSachMoi = db.Saches.OrderBy(n => n.GiaBan).Skip(20).Take(8).ToList();
            return View(lstSachMoi);
        }
    }
}