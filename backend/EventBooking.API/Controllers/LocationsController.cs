using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var locations = await _locationService.GetAllLocationsAsync();
        return Ok(locations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var location = await _locationService.GetLocationByIdAsync(id);
        if (location == null) return NotFound();
        return Ok(location);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLocationDto dto)
    {
        await _locationService.CreateLocationAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateLocationDto dto)
    {
        await _locationService.UpdateLocationAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _locationService.DeleteLocationAsync(id);
        return Ok();
    }
}