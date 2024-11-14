using System;
using System.Collections.Generic;

namespace G23NHNT.Models
{
    public partial class Amenity
    {
        public Amenity()
        {
            IdHouses = new HashSet<House>();
            IdRooms = new HashSet<Room>();
        }

        public int IdAmenity { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<House> IdHouses { get; set; }
        public virtual ICollection<Room> IdRooms { get; set; }
    }
}
