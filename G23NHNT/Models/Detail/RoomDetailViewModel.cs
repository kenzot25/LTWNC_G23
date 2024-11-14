using G23NHNT.Models;

namespace G23NHNT.Models
{
    public class RoomDetailViewModel
    {
        public Room Room { get; set; }
        public List<RoomDetail> RoomDetails { get; set; }
        public List<Amenity> IdAmenities { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }

}