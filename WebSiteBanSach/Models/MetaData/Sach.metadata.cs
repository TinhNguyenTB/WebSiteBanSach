﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSiteBanSach.Models
{
    [MetadataTypeAttribute(typeof(SachMetadata))]
    public partial class Sach
    {
        internal sealed class SachMetadata
        {
            [Display(Name ="Mã sách")]
            [Required(ErrorMessage ="{0} không được để trống")]
            public int MaSach { get; set; }
            [Display(Name = "Tên sách")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TenSach { get; set; }
            [Display(Name = "Giá bán")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<int> GiaBan { get; set; }
            [Display(Name = "Mô tả")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string MoTa { get; set; }
            [Display(Name = "Ảnh bìa")]
           
            public string AnhBia { get; set; }
            [Display(Name = "Ngày cập nhật")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
            [Display(Name = "Số lượng tồn")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<int> SoLuongTon { get; set; }
            [Display(Name = "Nhà xuất bản")]
            public Nullable<int> MaNXB { get; set; }
            [Display(Name = "Chủ đề")]
            public Nullable<int> MaChuDe { get; set; }
        }
    }
}