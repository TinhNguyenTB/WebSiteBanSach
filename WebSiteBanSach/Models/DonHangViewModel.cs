using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanSach.Models
{
    public class DonHangViewModel
    {
        public DonHang DonHang { get; set; }
        public List<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}