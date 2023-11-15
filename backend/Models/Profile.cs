public class Profile
{
    public int ProfileId { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Summary { get; set; }
    public string Instagram { get; set; }
    public string Facebook { get; set; }
    public int YearsOld { get; set; }

    public List<Framework> Frameworks { get; set; }
    public List<Hobby> Hobbies { get; set; }
}
