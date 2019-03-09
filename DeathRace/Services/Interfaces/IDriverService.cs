namespace DeathRace.Services.Interfaces
{
    public interface IDriverService
    {
        IEnumerable<Driver> GetAll();
        Driver Add(Driver newDriver);
        Driver GetById(int id);
        void Remove(int id);
    }
}
