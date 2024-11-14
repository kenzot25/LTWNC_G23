// IRoomRepository.cs
using G23NHNT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomWithDetailsAsync(int id);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
        Task<IEnumerable<Room>> GetRoomsByTypeAsync(string type);
        Task<List<Room>> GetRoomsByUserId(int userId);
    }
}

