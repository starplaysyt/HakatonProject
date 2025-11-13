namespace HakatonProject.Models;

public class UserInterest : IEntity
{
    public User User { get; set; }
    public Interest Interest { get; set; }
}