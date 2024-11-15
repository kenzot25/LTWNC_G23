using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G23NHNT.Models
{
    public partial class Account
    {
        public Account()
        {
            Houses = new HashSet<House>();
            Reviews = new HashSet<Review>();
        }

        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên đăng nhập phải dưới 50 ký tự.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải dưới 100 ký tự.")]
        public string Password { get; set; } = null!;

        [Range(1, 2, ErrorMessage = "Vai trò không hợp lệ.")]
        public int Role { get; set; }

        [Phone]
        [StringLength(15, ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string? PhoneNumber { get; set; }

        // Navigation properties
        public virtual ICollection<House> Houses { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
