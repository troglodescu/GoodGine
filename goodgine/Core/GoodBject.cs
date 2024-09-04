namespace GoodGine;

public class GoodBject
{
    internal GoodBject(string gName)
    {
        Name = gName;
    }

    public string Name { get; set; }

    internal List<GoodBject> Children { get; set; } = new List<GoodBject>();
    internal List<Component> Components { get; set; } = new List<Component>();

    public T GetComponent<T>() where T : Component
    {
        foreach (var component in Components)
        {
            var requiredComponent = component as T;
            if (requiredComponent != null) return requiredComponent;
        }
        return null;
    }

    internal void Update()
    {
        foreach (var component in Components)
        {
            component.Update();
        }

        foreach (var child in Children)
        {
            child.Update();
        }
    }

    internal void Awake()
    {
        foreach (var component in Components)
        {
            component.Awake();
        }

        foreach (var child in Children)
        {
            child.Awake();
        }
    }

    internal void Start()
    {
        foreach (var component in Components)
        {
            component.Start();
        }

        foreach (var child in Children)
        {
            child.Start();
        }
    }
}