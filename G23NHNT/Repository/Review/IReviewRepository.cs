using G23NHNT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetReviewsByHouseIdAsync(int houseId);
    Task AddReviewAsync(Review review);
}
