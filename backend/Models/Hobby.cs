public class Hobby
{
    public int HobbyId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
}