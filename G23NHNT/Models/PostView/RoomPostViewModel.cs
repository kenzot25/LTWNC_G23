using G23NHNT.Models;
using System.Collections.Generic;

namespace G23NHNT.ViewModels
{
    public class RoomPostViewModel
    {
        public Room Room { get; set; } = new Room();
        public RoomDetail RoomDetail { get; set; } = new RoomDetail();
        public List<Amenity> Amenities { get; set; } = new List<Amenity>();
        public List<int> SelectedAmenities { get; set; } = new List<int>();
    }
}
