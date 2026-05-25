using System;

namespace EventBooking.Application.DTOs;

public class CreateEventDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal TicketPrice { get; set; }
    public int Capacity { get; set; }
    public int LocationId { get; set; }
}