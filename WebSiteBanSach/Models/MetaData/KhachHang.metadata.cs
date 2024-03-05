using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSiteBanSach.Models
{
    [MetadataTypeAttribute(typeof(KhachHangMetadata))]
    public partial class KhachHang
    {
        internal sealed class KhachHangMetadata
        {
            [Display(Name = "Mã khách hàng")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public int MaKH { get; set; }
            [Display(Name = "Họ tên")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string HoTen { get; set; }
            [Display(Name = "Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TaiKhoan { get; set; }
            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string MatKhau { get; set; }
            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string Email { get; set; }
            [Display(Name = "Địa chỉ nhận hàng")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string DiaChi { get; set; }
            [Display(Name = "Số điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string DienThoai { get; set; }
            [Display(Name = "Giới tính")]
            [Required(ErrorMessage = "Vui lòng chọn {0}!")]
            public string GioiTinh { get; set; }
            [Display(Name = "Ngày sinh")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> NgaySinh { get; set; }
        }
    }
}