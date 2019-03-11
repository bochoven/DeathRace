using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeathRace.Models;
using DeathRace.Contexts;
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

        public async Task<Driver> GetById(int id)
        {
            return await _context.Drivers.Include(i => i.Cars)
                      .FirstOrDefaultAsync(i => i.DriverId == id);
        }

        public async Task UpdateById(int id, Driver driver)
        {
            _context.Entry(driver).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var itemToRemove = await _context.Drivers.SingleOrDefaultAsync(i => i.DriverId == id);
            if (itemToRemove != null)
            {
                _context.Drivers.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
