using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ev = await _eventService.GetEventByIdAsync(id);
        if (ev == null) return NotFound();
        return Ok(ev);
    }

    [HttpGet("location/{locationId}")]
    public async Task<IActionResult> GetByLocation(int locationId)
    {
        var events = await _eventService.GetEventsByLocationAsync(locationId);
        return Ok(events);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEventDto dto)
    {
        await _eventService.CreateEventAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateEventDto dto)
    {
        await _eventService.UpdateEventAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _eventService.DeleteEventAsync(id);
        return Ok();
    }
}