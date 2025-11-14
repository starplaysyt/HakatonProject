using HakatonProject.Models;

public class Event : IEntity
{
    public string Name { get; set; }
    public string Type { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
    public string Description { get; set; }

    public Place Place { get; set; }

    public User Owner { get; set; }
    
    public Interest Interest { get; set; }
}