// HouseRepository.cs
using G23NHNT.Models;
using G23NHNT.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly G23_NHNTContext _context;

        public HouseRepository(G23_NHNTContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<House>> GetAllHousesAsync()
        {
            return await _context.Houses
                .Include(h => h.HouseDetails)
                .Include(h => h.IdAmenities)
                .Include(h => h.IdUserNavigation)
                .ToListAsync();
        }
        public async Task<House> GetHouseWithDetailsAsync(int id)
        {
            return await _context.Houses
               .Include(h => h.HouseDetails)
               .Include(h => h.IdAmenities)
                .Include(h => h.IdUserNavigation)
               .Include(h => h.Reviews)
                   .ThenInclude(review => review.IdUserNavigation)
               .FirstOrDefaultAsync(h => h.IdHouse == id);
        }

        public async Task AddAsync(House house)
        {
            try
            {

                await _context.Houses.AddAsync(house);
                int affectedRows = await _context.SaveChangesAsync();

                if (affectedRows == 0)
                {
                    Console.WriteLine("Không có bản ghi nào được thêm vào cơ sở dữ liệu.");
                }
                else
                {
                    Console.WriteLine($"Đã thêm {affectedRows} bản ghi vào cơ sở dữ liệu.");
                }
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error: {dbEx.Message}");
                Console.WriteLine(dbEx.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error adding house: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
        public async Task UpdateAsync(House house)
        {
            if (_context.Entry(house).State == EntityState.Detached)
            {
                _context.Houses.Attach(house);
            }

            _context.Entry(house).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var house = await _context.Houses.FindAsync(id);
            if (house != null)
            {
                _context.Houses.Remove(house);
                await _context.SaveChangesAsync();
            }
        }
         public async Task<List<House>> GetHousesByUserId(int userId)
        {
            return await _context.Houses
                            .Include(h => h.HouseDetails)
                           .Where(h => h.IdUser == userId)
                           .ToListAsync();
        }
        public async Task<IEnumerable<House>> GetHousesByCategoryAsync(string category)
        {
            return await _context.Houses
                .Where(h => h.Category == category)
                .Include(h => h.IdUserNavigation)
                .ToListAsync();
        }
    }
}
