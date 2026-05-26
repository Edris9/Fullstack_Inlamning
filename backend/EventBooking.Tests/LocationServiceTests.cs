using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using EventBooking.Application.Services;
using EventBooking.Domain;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBooking.Tests;

public class LocationServiceTests
{
    private readonly ILocationRepository _locationRepository;
    private readonly LocationService _locationService;

    public LocationServiceTests()
    {
        _locationRepository = Substitute.For<ILocationRepository>();
        _locationService = new LocationService(_locationRepository);
    }

    // Test 1 - Happy path: Hämta alla locations
    [Fact]
    public async Task GetAllLocationsAsync_ReturnsAllLocations()
    {
        // Arrange
        var locations = new List<Location>
        {
            new Location { Id = 1, Name = "Avicii Arena", City = "Stockholm", Address = "Globentorget 2" },
            new Location { Id = 2, Name = "Scandinavium", City = "Göteborg", Address = "Valhallagatan 1" }
        };
        _locationRepository.GetAllAsync().Returns(locations);

        // Act
        var result = await _locationService.GetAllLocationsAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    // Test 2 - Happy path: Hämta location med id
    [Fact]
    public async Task GetLocationByIdAsync_ExistingId_ReturnsLocation()
    {
        // Arrange
        var location = new Location { Id = 1, Name = "Avicii Arena", City = "Stockholm", Address = "Globentorget 2" };
        _locationRepository.GetByIdAsync(1).Returns(location);

        // Act
        var result = await _locationService.GetLocationByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Avicii Arena", result.Name);
    }

    // Test 3 - Edge case: Location finns inte
    [Fact]
    public async Task GetLocationByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        _locationRepository.GetByIdAsync(99).Returns((Location?)null);

        // Act
        var result = await _locationService.GetLocationByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    // Test 4 - Happy path: Skapa location
    [Fact]
    public async Task CreateLocationAsync_ValidDto_CallsRepository()
    {
        // Arrange
        var dto = new CreateLocationDto { Name = "Avicii Arena", City = "Stockholm", Address = "Globentorget 2" };

        // Act
        await _locationService.CreateLocationAsync(dto);

        // Assert
        await _locationRepository.Received(1).AddAsync(Arg.Any<Location>());
    }

    // Test 5 - Happy path: Ta bort location
    [Fact]
    public async Task DeleteLocationAsync_ValidId_CallsRepository()
    {
        // Arrange
        int id = 1;

        // Act
        await _locationService.DeleteLocationAsync(id);

        // Assert
        await _locationRepository.Received(1).DeleteAsync(id);
    }

    // Test 6 - Edge case: Uppdatera location som inte finns
    [Fact]
    public async Task UpdateLocationAsync_NonExistingId_DoesNotCallUpdate()
    {
        // Arrange
        _locationRepository.GetByIdAsync(99).Returns((Location?)null);
        var dto = new CreateLocationDto { Name = "Test", City = "Test", Address = "Test" };

        // Act
        await _locationService.UpdateLocationAsync(99, dto);

        // Assert
        await _locationRepository.DidNotReceive().UpdateAsync(Arg.Any<Location>());
    }

    // Test 7 - Happy path: Uppdatera location
    [Fact]
    public async Task UpdateLocationAsync_ExistingId_CallsUpdate()
    {
        // Arrange
        var location = new Location { Id = 1, Name = "Gamla namnet", City = "Stockholm", Address = "Gammal adress" };
        _locationRepository.GetByIdAsync(1).Returns(location);
        var dto = new CreateLocationDto { Name = "Nya namnet", City = "Stockholm", Address = "Ny adress" };

        // Act
        await _locationService.UpdateLocationAsync(1, dto);

        // Assert
        await _locationRepository.Received(1).UpdateAsync(Arg.Any<Location>());
    }
}