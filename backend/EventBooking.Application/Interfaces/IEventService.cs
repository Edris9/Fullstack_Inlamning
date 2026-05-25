using EventBooking.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllEventsAsync();
    Task<EventDto?> GetEventByIdAsync(int id);
    Task<IEnumerable<EventDto>> GetEventsByLocationAsync(int locationId);
    Task CreateEventAsync(CreateEventDto dto);
    Task UpdateEventAsync(int id, CreateEventDto dto);
    Task DeleteEventAsync(int id);
}