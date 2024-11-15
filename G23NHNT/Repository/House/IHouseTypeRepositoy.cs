using G23NHNT.Models;

namespace G23NHNT.Repositories
{
    public interface IHouseTypeRepository
    {
        Task<IEnumerable<HouseType>> GetAllHouseTypes();
        String GetHouseTypeName(int id);
    }
}
