// IHouseRepository.cs
using G23NHNT.Models;
using G23NHNT.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public interface IHouseRepository
    {
        Task<IEnumerable<House>> GetAllHousesAsync();
        Task<House> GetHouseWithDetailsAsync(int id);
        Task AddAsync(House house);
        Task UpdateAsync(House house);
        Task DeleteAsync(int id);
        Task<IEnumerable<House>> GetHousesByCategoryAsync(string category);
        Task<List<House>> GetHousesByUserId(int userId);
    }
}
