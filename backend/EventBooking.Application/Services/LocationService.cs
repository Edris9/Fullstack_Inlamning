using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using EventBooking.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
    {
        var locations = await _locationRepository.GetAllAsync();
        return locations.Select(l => new LocationDto
        {
            Id = l.Id,
            Name = l.Name,
            City = l.City,
            Address = l.Address
        });
    }

    public async Task<LocationDto?> GetLocationByIdAsync(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null) return null;
        return new LocationDto
        {
            Id = location.Id,
            Name = location.Name,
            City = location.City,
            Address = location.Address
        };
    }

    public async Task CreateLocationAsync(CreateLocationDto dto)
    {
        var location = new Location
        {
            Name = dto.Name,
            City = dto.City,
            Address = dto.Address
        };
        await _locationRepository.AddAsync(location);
    }

    public async Task UpdateLocationAsync(int id, CreateLocationDto dto)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null) return;
        location.Name = dto.Name;
        location.City = dto.City;
        location.Address = dto.Address;
        await _locationRepository.UpdateAsync(location);
    }

    public async Task DeleteLocationAsync(int id)
    {
        await _locationRepository.DeleteAsync(id);
    }
}