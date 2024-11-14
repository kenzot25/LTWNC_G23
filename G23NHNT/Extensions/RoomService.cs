using G23NHNT.Models;
using G23NHNT.Repositories;
using G23NHNT.ViewModels;
using System;
using System.Threading.Tasks;

namespace G23NHNT.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomDetailRepository _roomDetailRepository;
        private readonly IAmenityRepository _amenityRepository;

        public RoomService(IRoomRepository roomRepository, IRoomDetailRepository roomDetailRepository, IAmenityRepository amenityRepository)
        {
            _roomRepository = roomRepository;
            _roomDetailRepository = roomDetailRepository;
            _amenityRepository = amenityRepository;
        }

        public async Task<bool> CreateRoomAsync(RoomPostViewModel model)
        {
            try
            {
                await _roomRepository.AddAsync(model.Room);

                model.RoomDetail.IdRoom = model.Room.IdRoom;
                await _roomDetailRepository.AddAsync(model.RoomDetail);

                foreach (var amenityId in model.SelectedAmenities)
                {
                    var amenity = await _amenityRepository.GetAmenityByIdAsync(amenityId);
                    if (amenity != null)
                    {
                        model.Room.IdAmenities.Add(amenity); 
                    }
                }
                await _roomRepository.UpdateAsync(model.Room);

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating room: {ex.Message}");
                return false; 
            }
        }
    }
}
