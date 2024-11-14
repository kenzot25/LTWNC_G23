using G23NHNT.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReviewRepository : IReviewRepository
{
    private readonly G23_NHNTContext _context;

    public ReviewRepository(G23_NHNTContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Review>> GetReviewsByHouseIdAsync(int houseId)
    {
        return await _context.Reviews
            .Where(r => r.IdHouse == houseId)
            .Include(r => r.IdUserNavigation)
            .ToListAsync();
    }
    public async Task<IEnumerable<Review>> GetReviewsByRoomIdAsync(int roomId)
    {
        return await _context.Reviews
            .Where(r => r.IdRoom == roomId)
            .Include(r => r.IdUserNavigation)
            .ToListAsync();
    }
    public async Task AddReviewAsync(Review review)
    {
        try
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding review: " + ex.Message);
            throw;
        }
    }
}
