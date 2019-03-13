using DeathRace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeathRace.Repository
{
    public interface IDriverRepository
    {
        Task Add(DriverDto item);
        Task<IEnumerable<DriverDto>> GetAllDrivers();
        Task<DriverDto> GetById(int id);
        Task Remove(int id);
        Task UpdateById(int id, DriverDto driver);
    }
}
