using System;
using System.Collections.Generic;

namespace G23NHNT.Models
{
    public partial class Room
    {
        public Room()
        {
            Reviews = new HashSet<Review>();
            RoomDetails = new HashSet<RoomDetail>();
            IdAmenities = new HashSet<Amenity>();
        }

        public int IdRoom { get; set; }
        public string NameRoom { get; set; } = null!;
        public string TypeRoom { get; set; } = null!;
        public int IdUser { get; set; }

        public virtual Account? IdUserNavigation { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<RoomDetail> RoomDetails { get; set; }
        public virtual ICollection<Amenity> IdAmenities { get; set; }
    }
}
