using System.Collections.Generic;
using System.Threading.Tasks;
using DeathRace.Models;
using DeathRace.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<IEnumerable<CarDto>> GetAllCars(int? startyear)
        {
           var cars = from c in _context.Cars
              select c;

           if (startyear != null)
           {
               return await cars.Select(x=> new CarDto(x))
                  .Where(c => c.Year >= startyear)
                  .ToListAsync();
           }

           return await cars.Select(x=> new CarDto(x)).ToListAsync();
        }

        public async Task<CarDto> GetById(int id)
        {
          return await _context.Cars.Select(x => new CarDto(x))
              .FirstOrDefaultAsync(i => i.CarId == id);
        }

        public async Task UpdateById(int id, Car car)
        {            
            var carToUpdate = await _context.Cars.SingleOrDefaultAsync(r => r.CarId == id);
            if (carToUpdate != null)
            {
                carToUpdate.Brand = car.Brand;
                carToUpdate.Model = car.Model;
                carToUpdate.Type = car.Type;
                carToUpdate.Year = car.Year;
                carToUpdate.DriverId = car.DriverId;
                await _context.SaveChangesAsync();
            }
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
