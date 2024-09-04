using GoodGine.TextParsing;

namespace GoodGine;

public abstract class Component
{
    public GoodBject GoodBject { get; private set; }

    public virtual void Update()
    { }

    public virtual void Awake()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void Parse(TextParser parser, int tabCount)
    { }

    public T GetComponent<T>() where T : Component
    {
        foreach (var c in GoodBject.Components)
        {
            var searchComponent = c as T;
            if (searchComponent != null) return searchComponent;
        }
        return null;
    }

    internal void SetGoodBject(GoodBject bject)
    {
        GoodBject = bject;
    }
}