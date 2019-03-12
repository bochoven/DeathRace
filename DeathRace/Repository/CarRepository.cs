using System.Collections.Generic;
using System.Threading.Tasks;
using DeathRace.Models;
using DeathRace.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace DeathRace.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DeathRaceContext _context;
        private readonly IMapper _mapper;

        public CarRepository(DeathRaceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(CarDto newCar)
        {
            await _context.Cars.AddAsync(_mapper.Map<Car>(newCar));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarDto>> GetAllCars(int? startyear)
        {
           var cars = from c in _context.Cars
              select c;

           if (startyear != null)
           {
               return await cars
                  .Where(c => c.Year >= startyear)
                  .ProjectTo<CarDto>(_mapper.ConfigurationProvider)
                  .ToListAsync();
           }

           return await cars.ProjectTo<CarDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<CarDto> GetById(int id)
        {
          return await _context.Cars.ProjectTo<CarDto>(_mapper.ConfigurationProvider)
              .FirstOrDefaultAsync(i => i.CarId == id);
        }

        public async Task UpdateById(int id, CarDto car)
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
