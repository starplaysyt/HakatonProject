public class Owner : IEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Job { get; set; }
    public List<Event> Events { get; set; }
    public Faculty OwnerFaculty { get; set; }
}
