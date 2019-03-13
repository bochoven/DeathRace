using System.Collections.Generic;
using System.Threading.Tasks;
using DeathRace.Models;
using DeathRace.Contexts;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace DeathRace.Repository
{
    public class DriverRepository : IDriverRepository
    {
        private readonly DeathRaceContext _context;
        private readonly IMapper _mapper;

        public DriverRepository(DeathRaceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(DriverDto newDriver)
        {
            await _context.Drivers.AddAsync(_mapper.Map<Driver>(newDriver));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DriverDto>> GetAllDrivers()
        {
           return await _context.Drivers
                .Include(i => i.Cars)
                .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<DriverDto> GetById(int id)
        {
            return await _context.Drivers
                .Include(i => i.Cars)
                .ProjectTo<DriverDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(i => i.DriverId == id);
        }

        // public async Task UpdateById(int id, Driver driver)
        // {
        //     _context.Entry(driver).State = EntityState.Modified;
        //     await _context.SaveChangesAsync();
        // }
        
        public async Task UpdateById(int id, DriverDto driver)
        {            
            var driverToUpdate = await _context.Drivers.SingleOrDefaultAsync(r => r.DriverId == id);
            if (driverToUpdate != null)
            {
                driverToUpdate.GivenName = driver.GivenName;
                driverToUpdate.Preposition = driver.Preposition;
                driverToUpdate.LastName = driver.LastName;
                driverToUpdate.DOB = driver.DOB;
                await _context.SaveChangesAsync();
            }
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
