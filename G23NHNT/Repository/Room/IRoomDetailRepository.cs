using G23NHNT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public interface IRoomDetailRepository
    {
        Task<IEnumerable<RoomDetail>> GetAllRoomDetailsAsync();
        Task<RoomDetail> GetRoomDetailByIdAsync(int id);
        Task AddAsync(RoomDetail roomDetail);
        Task UpdateAsync(RoomDetail roomDetail);
        Task DeleteAsync(int id);
    }
}
