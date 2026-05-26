using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;
using EventBooking.Application.Services;
using EventBooking.Domain;
using NSubstitute;

namespace EventBooking.Tests;

public class EventServiceTests
{
    private readonly IEventRepository _eventRepository;
    private readonly EventService _eventService;

    public EventServiceTests()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventService = new EventService(_eventRepository);
    }

    // Test 1 - Happy path: Hämta alla events
    [Fact]
    public async Task GetAllEventsAsync_ReturnsAllEvents()
    {
        // Arrange
        var events = new List<Event>
        {
            new Event { Id = 1, Title = "Konsert", Description = "Rolig kväll", Date = DateTime.Now, TicketPrice = 100, Capacity = 500, LocationId = 1 },
            new Event { Id = 2, Title = "Festival", Description = "Utomhus", Date = DateTime.Now, TicketPrice = 200, Capacity = 1000, LocationId = 1 }
        };
        _eventRepository.GetAllAsync().Returns(events);

        // Act
        var result = await _eventService.GetAllEventsAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    // Test 2 - Happy path: Hämta event med id
    [Fact]
    public async Task GetEventByIdAsync_ExistingId_ReturnsEvent()
    {
        // Arrange
        var ev = new Event { Id = 1, Title = "Konsert", Description = "Rolig kväll", Date = DateTime.Now, TicketPrice = 100, Capacity = 500, LocationId = 1 };
        _eventRepository.GetByIdAsync(1).Returns(ev);

        // Act
        var result = await _eventService.GetEventByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Konsert", result.Title);
    }

    // Test 3 - Edge case: Event finns inte
    [Fact]
    public async Task GetEventByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        _eventRepository.GetByIdAsync(99).Returns((Event?)null);

        // Act
        var result = await _eventService.GetEventByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    // Test 4 - Happy path: Skapa event
    [Fact]
    public async Task CreateEventAsync_ValidDto_CallsRepository()
    {
        // Arrange
        var dto = new CreateEventDto { Title = "Konsert", Description = "Rolig kväll", Date = DateTime.Now, TicketPrice = 100, Capacity = 500, LocationId = 1 };

        // Act
        await _eventService.CreateEventAsync(dto);

        // Assert
        await _eventRepository.Received(1).AddAsync(Arg.Any<Event>());
    }

    // Test 5 - Happy path: Ta bort event
    [Fact]
    public async Task DeleteEventAsync_ValidId_CallsRepository()
    {
        // Arrange
        int id = 1;

        // Act
        await _eventService.DeleteEventAsync(id);

        // Assert
        await _eventRepository.Received(1).DeleteAsync(id);
    }

    // Test 6 - Edge case: Uppdatera event som inte finns
    [Fact]
    public async Task UpdateEventAsync_NonExistingId_DoesNotCallUpdate()
    {
        // Arrange
        _eventRepository.GetByIdAsync(99).Returns((Event?)null);
        var dto = new CreateEventDto { Title = "Test", Description = "Test", Date = DateTime.Now, TicketPrice = 100, Capacity = 100, LocationId = 1 };

        // Act
        await _eventService.UpdateEventAsync(99, dto);

        // Assert
        await _eventRepository.DidNotReceive().UpdateAsync(Arg.Any<Event>());
    }

    // Test 7 - Happy path: Uppdatera event
    [Fact]
    public async Task UpdateEventAsync_ExistingId_CallsUpdate()
    {
        // Arrange
        var ev = new Event { Id = 1, Title = "Gamla titeln", Description = "Gammal", Date = DateTime.Now, TicketPrice = 100, Capacity = 500, LocationId = 1 };
        _eventRepository.GetByIdAsync(1).Returns(ev);
        var dto = new CreateEventDto { Title = "Nya titeln", Description = "Ny", Date = DateTime.Now, TicketPrice = 200, Capacity = 600, LocationId = 1 };

        // Act
        await _eventService.UpdateEventAsync(1, dto);

        // Assert
        await _eventRepository.Received(1).UpdateAsync(Arg.Any<Event>());
    }
}