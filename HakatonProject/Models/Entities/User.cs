namespace HakatonProject.Models;

public class User : IEntity
{
    public string Name { get; set; }
    public string Login { get; set; }

    public string Password { get; set; }

    public string UserGroup { get; set; } //Role in claim

    public string Job { get; set; }

    public Faculty UserFaculty { get; set; }
}