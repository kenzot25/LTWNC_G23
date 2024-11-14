using G23NHNT.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public class RoomDetailRepository : IRoomDetailRepository
    {
        private readonly G23_NHNTContext _context;

        public RoomDetailRepository(G23_NHNTContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoomDetail>> GetAllRoomDetailsAsync()
        {
            return await _context.RoomDetails.Include(r => r.IdRoomNavigation).ToListAsync();
        }

        public async Task<RoomDetail> GetRoomDetailByIdAsync(int id)
        {
            return await _context.RoomDetails
                .Include(r => r.IdRoomNavigation)
                .FirstOrDefaultAsync(r => r.IdRoomDetail == id);
        }

        public async Task AddAsync(RoomDetail roomDetail)
        {
            await _context.RoomDetails.AddAsync(roomDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomDetail roomDetail)
        {
            _context.RoomDetails.Update(roomDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var roomDetail = await _context.RoomDetails.FindAsync(id);
            if (roomDetail != null)
            {
                _context.RoomDetails.Remove(roomDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
