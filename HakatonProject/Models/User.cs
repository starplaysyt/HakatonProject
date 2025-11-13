namespace HakatonProject.Models;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Interest> Interests { get; set; }
    public List<Event> Events { get; set; }
    public long FacID { get; set; }
}
