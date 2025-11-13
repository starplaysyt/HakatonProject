namespace HakatonProject.Models;

public class User : IEntity
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Interest> Interests { get; set; }
    public List<Event> Events { get; set; }
    public List<UserContact> Contacts { get; set; }
    public Faculty UserFaculty { get; set; }
}
