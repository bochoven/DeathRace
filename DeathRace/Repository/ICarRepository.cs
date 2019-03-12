using DeathRace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeathRace.Repository
{
    public interface ICarRepository
    {
        Task Add(Car item);
        Task<IEnumerable<CarDto>> GetAllCars(int? startyear);
        Task<CarDto> GetById(int id);
        Task Remove(int id);
        Task UpdateById(int id, Car car);
    }
}
