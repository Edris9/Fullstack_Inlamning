namespace EventBooking.Domain;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    // Navigation property (1-många)
    public ICollection<Event> Events { get; set; } = new List<Event>();
}