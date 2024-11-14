using G23NHNT.Models;
using G23NHNT.Repositories;
using G23NHNT.ViewModels;
using System;
using System.Threading.Tasks;

namespace G23NHNT.Services
{
    public class HouseService
    {
        private readonly IHouseRepository _houseRepository;
        private readonly IHouseDetailRepository _houseDetailRepository;
        private readonly IAmenityRepository _amenityRepository;

        public HouseService(IHouseRepository houseRepository, IHouseDetailRepository houseDetailRepository, IAmenityRepository amenityRepository)
        {
            _houseRepository = houseRepository;
            _houseDetailRepository = houseDetailRepository;
            _amenityRepository = amenityRepository;
        }

        public async Task<bool> CreateHouseAsync(HousePostViewModel model)
        {
            try
            {
                await _houseRepository.AddAsync(model.House);

                model.HouseDetail.IdHouse = model.House.IdHouse;
                await _houseDetailRepository.AddAsync(model.HouseDetail);

                foreach (var amenityId in model.SelectedAmenities)
                {
                    var amenity = await _amenityRepository.GetAmenityByIdAsync(amenityId);
                    if (amenity != null)
                    {
                        model.House.IdAmenities.Add(amenity);
                    }
                }
                await _houseRepository.UpdateAsync(model.House);

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating house: {ex.Message}");
                return false; 
            }
        }
    }
}
