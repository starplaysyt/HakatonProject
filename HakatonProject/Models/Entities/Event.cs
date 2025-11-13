public class Event : IEntity
{
    public string Name { get; set; }
    public string Type { get; set;}
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
    public List<Place> Places { get; set; }
    public long OrganizationsId { get; set; }
}
