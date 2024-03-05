using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f)
        {
            string keyWord = f["txtTimKiem"].ToString();
            List<Sach> lst = db.Saches.Where(n => n.TenSach.Contains(keyWord)).ToList();

            if (lst.Count == 0)
            {
                ViewBag.Thongbao = "Không tìm thấy sách nào";
            }
            else
            {
                ViewBag.Thongbao = "Đã tìm thấy " + lst.Count + " kết quả";
            }

            return View(lst);
        }
    }

}