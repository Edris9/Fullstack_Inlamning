using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using EventBooking.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return events.Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Date = e.Date,
            TicketPrice = e.TicketPrice,
            Capacity = e.Capacity,
            LocationId = e.LocationId,
            LocationName = e.Location?.Name ?? string.Empty
        });
    }

    public async Task<EventDto?> GetEventByIdAsync(int id)
    {
        var e = await _eventRepository.GetByIdAsync(id);
        if (e == null) return null;
        return new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Date = e.Date,
            TicketPrice = e.TicketPrice,
            Capacity = e.Capacity,
            LocationId = e.LocationId,
            LocationName = e.Location?.Name ?? string.Empty
        };
    }

    public async Task<IEnumerable<EventDto>> GetEventsByLocationAsync(int locationId)
    {
        var events = await _eventRepository.GetEventsByLocationAsync(locationId);
        return events.Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Date = e.Date,
            TicketPrice = e.TicketPrice,
            Capacity = e.Capacity,
            LocationId = e.LocationId,
            LocationName = e.Location?.Name ?? string.Empty
        });
    }

    public async Task CreateEventAsync(CreateEventDto dto)
    {
        var ev = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            Date = dto.Date,
            TicketPrice = dto.TicketPrice,
            Capacity = dto.Capacity,
            LocationId = dto.LocationId
        };
        await _eventRepository.AddAsync(ev);
    }

    public async Task UpdateEventAsync(int id, CreateEventDto dto)
    {
        var ev = await _eventRepository.GetByIdAsync(id);
        if (ev == null) return;
        ev.Title = dto.Title;
        ev.Description = dto.Description;
        ev.Date = dto.Date;
        ev.TicketPrice = dto.TicketPrice;
        ev.Capacity = dto.Capacity;
        ev.LocationId = dto.LocationId;
        await _eventRepository.UpdateAsync(ev);
    }

    public async Task DeleteEventAsync(int id)
    {
        await _eventRepository.DeleteAsync(id);
    }
}