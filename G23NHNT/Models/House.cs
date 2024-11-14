using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G23NHNT.Models
{
    public partial class House
    {
        public House()
        {
            HouseDetails = new HashSet<HouseDetail>();
            Reviews = new HashSet<Review>();
            IdAmenities = new HashSet<Amenity>();
        }

        public int IdHouse { get; set; }

        [Required(ErrorMessage = "Tên nhà trọ là bắt buộc.")]
        public string NameHouse { get; set; } = null!;

        public int? QuantityRoom { get; set; }

        [Required(ErrorMessage = "Loại nhà trọ là bắt buộc.")]
        public string Category { get; set; } = null!;

        public int IdUser { get; set; }

        public virtual Account? IdUserNavigation { get; set; }
        public virtual ICollection<HouseDetail> HouseDetails { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Amenity> IdAmenities { get; set; }
    }
}
