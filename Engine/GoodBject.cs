namespace Good;

public class GoodBject
{
    public GoodBject(string gName)
    {
        Name = gName;
    }

    public string Name { get; set; }

    public List<GoodBject> Children { get; set; } = new List<GoodBject>();
    public List<Component> Components { get; set; } = new List<Component>();
}