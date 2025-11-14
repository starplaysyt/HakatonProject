public class CreateEventDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long PlaceId { get; set; }
    public string Type { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
}