using EventBooking.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Application.Interfaces;

public interface ILocationService
{
    Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
    Task<LocationDto?> GetLocationByIdAsync(int id);
    Task CreateLocationAsync(CreateLocationDto dto);
    Task UpdateLocationAsync(int id, CreateLocationDto dto);
    Task DeleteLocationAsync(int id);
}