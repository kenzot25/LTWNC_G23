using System;
using G23NHNT.Models;
using G23NHNT.Repositories;
using Microsoft.EntityFrameworkCore;

namespace G23NHNT.Repository.House
{
    public class HouseTypeRepository : IHouseTypeRepository
    {
        private readonly G23_NHNTContext _context;

        public HouseTypeRepository(G23_NHNTContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HouseType>> GetAllHouseTypes()
        {
            return await _context.HouseType.ToListAsync();
        }

        public String GetHouseTypeName(int id)
        {
            return _context.HouseType.FirstOrDefault(e => e.IdHouseType == id).Name;
        }
    }
}

