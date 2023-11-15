public class Framework
{
    public int FrameworkId { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public int Year { get; set; }

    public int Quantity {get; set; }

    public int ProfileId { get; set; }
    public Profile Profile { get; set; }
}