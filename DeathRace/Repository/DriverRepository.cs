using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeathRace.Models;
using Microsoft.EntityFrameworkCore;

namespace DeathRace.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DeathRaceContext _context;
        
        public DriverRepository(DeathRaceContext context)
        {
            _context = context;
        }

        public async Task Add(Driver newDriver)
        {            
            await _context.Drivers.AddAsync(newDriver);
            await _context.SaveChangesAsync();
        }

             
        public async Task<IEnumerable<Driver>> GetAllDrivers()
        {
           return await _context.Drivers.Include(i => i.Cars).ToListAsync();
        }

        public async Task GetById(int id)
                {
            throw new NotImplementedException();
        }
        
        public async Task UpdateById(int id)
        {
            throw new NotImplementedException();
        }
     
        public async Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}