using AutoMapper;
using DeathRace.Models;

public class MappingProfile : Profile {
    public MappingProfile() {
        // Add as many of these lines as you need to map your objects
        CreateMap<Driver, DriverDto>();
        CreateMap<DriverDto, Driver>();
        CreateMap<Car, CarDto>();
        CreateMap<CarDto, Car>();
    }
}
