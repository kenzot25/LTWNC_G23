using System;
using System.ComponentModel.DataAnnotations;

namespace G23NHNT.Models
{
    public partial class HouseDetail
    {
        public int IdHouseDetail { get; set; }
        public int IdHouse { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        public string Address { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "Giá thuê phải là một số dương.")]
        public decimal Price { get; set; }

        public double DienTich { get; set; }

        [Required(ErrorMessage = "Tiền điện là bắt buộc.")]
        public string TienDien { get; set; } = null!;

        [Required(ErrorMessage = "Tiền nước là bắt buộc.")]
        public string TienNuoc { get; set; } = null!;

        [Required(ErrorMessage = "Tiền dịch vụ là bắt buộc.")]
        public string TienDv { get; set; } = null!;

        public string? Describe { get; set; }
        public string Status { get; set; } = null!;
        public string? Image { get; set; }
        public DateTime TimePost { get; set; }

        public virtual House? IdHouseNavigation { get; set; }
    }
}
