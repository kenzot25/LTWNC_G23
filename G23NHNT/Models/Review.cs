using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace G23NHNT.Models
{
    public partial class Review
    {
        [Key]
        public int IdReview { get; set; }

        [Required(ErrorMessage = "IdUser không được để trống.")]
        public int IdUser { get; set; }

        public int? IdHouse { get; set; }
        public int? IdRoom { get; set; }

        [Range(1, 5, ErrorMessage = "Đánh giá phải nằm trong khoảng từ 1 đến 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Nội dung đánh giá không được để trống.")]
        [StringLength(500, ErrorMessage = "Nội dung đánh giá không được vượt quá 500 ký tự.")]
        public string? Content { get; set; }

        public DateTime? ReviewDate { get; set; }

        // Foreign Key for House
        [ForeignKey("IdHouse")]
        public virtual House? IdHouseNavigation { get; set; }

        // Foreign Key for User
        [ForeignKey("IdUser")]
        public virtual Account? IdUserNavigation { get; set; } = null!;
    }
}
