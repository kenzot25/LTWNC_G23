using G23NHNT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public interface IHouseDetailRepository
    {
        Task<IEnumerable<HouseDetail>> GetAllHouseDetailsAsync();
        Task<HouseDetail> GetHouseDetailByIdAsync(int id);
        Task AddAsync(HouseDetail houseDetail);
        Task UpdateAsync(HouseDetail houseDetail);
        Task DeleteAsync(int id);
    }
}
