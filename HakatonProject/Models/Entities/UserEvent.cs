namespace HakatonProject.Models;

public class UserEvent : IEntity
{
    public bool Participated { get; set; }

    public User User { get; set; }

    public Event Event { get; set; }
}