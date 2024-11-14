// RoomRepository.cs
using G23NHNT.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly G23_NHNTContext _context;

        public RoomRepository(G23_NHNTContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms
                .Include(r => r.RoomDetails)
                .Include(r => r.IdUserNavigation) 
                .ToListAsync();
        }

        public async Task<Room> GetRoomWithDetailsAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.RoomDetails)
                .Include(r => r.IdAmenities)
                .Include(r => r.IdUserNavigation) 
                .Include(r => r.Reviews)
                    .ThenInclude(review => review.IdUserNavigation)
                .FirstOrDefaultAsync(r => r.IdRoom == id);
        }

        public async Task AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Room>> GetRoomsByUserId(int userId)
        {
            return await _context.Rooms
                           .Include(r => r.RoomDetails)
                           .Where(r => r.IdUser == userId)
                           .ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetRoomsByTypeAsync(string type)
        {
            return await _context.Rooms
                .Where(r => r.TypeRoom == type)
                .Include(r => r.IdUserNavigation)
                .ToListAsync();
        }
    }
}
