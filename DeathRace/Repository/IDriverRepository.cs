using DeathRace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeathRace.Repository
{
    public interface IDriverRepository
    {
        Task Add(Driver item);
        Task<IEnumerable<Driver>> GetAllDrivers();
        Task<Driver> GetById(int id);
        Task Remove(int id);
        Task UpdateById(int id, Driver driver);
    }
}