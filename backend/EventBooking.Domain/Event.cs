using System;

namespace EventBooking.Domain;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal TicketPrice { get; set; }
    public int Capacity { get; set; }

    // Foreignkey (många-1)
    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
}