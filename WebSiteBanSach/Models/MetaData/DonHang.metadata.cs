using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSiteBanSach.Models
{
    [MetadataTypeAttribute(typeof(DonHangMetadata))]
    public partial class DonHang
    {
        internal sealed class DonHangMetadata
        {
            [Display(Name = "Mã đơn hàng")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public int MaDonHang { get; set; }
            [Display(Name = "Đã thanh toán")]
           
            public Nullable<int> DaThanhToan { get; set; }
            [Display(Name = "Tình trạng giao hàng")]
           
            public Nullable<int> TinhTrangGiaoHang { get; set; }
            [Display(Name = "Ngày đặt")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> NgayDat { get; set; }
            [Display(Name = "Ngày giao")]
           
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> NgayGiao { get; set; }
            [Display(Name = "Khách hàng")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<int> MaKH { get; set; }
        }
    }
}