using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class NhaXuatBanController : Controller
    {
        // GET: NhaXuatBan
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public PartialViewResult NhaXuatBanPartial()
        {
            return PartialView(db.NhaXuatBans.OrderBy(n => n.TenNXB).ToList());
        }
        public ViewResult SachTheoNXB(int maNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == maNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.SelectedNXB = maNXB;
            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == maNXB).OrderBy(n => n.GiaBan).ToList();
           
            return View(lstSach);
        }
    }
}