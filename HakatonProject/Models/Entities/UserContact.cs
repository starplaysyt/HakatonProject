namespace HakatonProject.Models;

public class UserContact : IEntity
{
    public User User { get; set; }

    public string Type { get; set; }

    public string Value { get; set; }
}