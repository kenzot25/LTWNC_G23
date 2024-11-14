using System;
using System.Collections.Generic;

namespace G23NHNT.Models
{
    public partial class RoomDetail
    {
        public int IdRoomDetail { get; set; }
        public int IdRoom { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; } = null!;
        public double DienTich { get; set; }
        public string TienDien { get; set; } = null!;
        public string TienNuoc { get; set; } = null!;
        public string TienDv { get; set; } = null!;
        public string? Describe { get; set; }
        public string Status { get; set; } = null!;
        public string? Image { get; set; }
        public DateTime TimePost { get; set; }

        public virtual Room? IdRoomNavigation { get; set; } = null!;
    }
}
