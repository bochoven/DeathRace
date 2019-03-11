using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeathRace.Models;
using DeathRace.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DeathRace.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DeathRaceContext _context;

        public CarRepository(DeathRaceContext context)
        {
            _context = context;
        }

        public async Task Add(Car newCar)
        {
            await _context.Cars.AddAsync(newCar);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
           return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetById(int id)
        {
            return await _context.Cars
                      .FirstOrDefaultAsync(i => i.CarId == id);
        }

        public async Task UpdateById(int id, Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var itemToRemove = await _context.Cars.SingleOrDefaultAsync(i => i.CarId == id);
            if (itemToRemove != null)
            {
                _context.Cars.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
