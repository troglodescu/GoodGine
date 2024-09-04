using GoodGine.TextParsing;
using static GoodGine.TextParsing.TextParser;

namespace GoodGine;

public class Scene
{
    private TextParser parser;
    private Func<string, Type> typeFinder;

    private bool shouldQuit = false;

    internal Scene(Func<string, Type> typeFinder)
    {
        this.typeFinder = typeFinder;
    }

    internal bool ShouldQuit => shouldQuit;
    internal List<GoodBject> GoodBjects { get; set; } = new List<GoodBject>();

    internal void Load(string path)
    {
        parser = CreateParserForFile(path);

        GoodBjects.Clear();
        var tabCount = 0;
        while (!parser.HasReachedEnd)
        {
            ParseGoodBjectRecursively(tabCount, null);
        }

        foreach (var go in GoodBjects)
        {
            go.Awake();
        }
        foreach (var go in GoodBjects)
        {
            go.Start();
        }
    }

    internal void Quit()
    {
        shouldQuit = true;
    }

    internal void Update()
    {
        foreach (var go in GoodBjects)
        {
            go.Update();
        }
    }

    private void ParseGoodBjectRecursively(int tabCount, GoodBject parent)
    {
        parser.ConsumeTabs(tabCount);
        parser.Consume("GoodBject");
        parser.Consume(SPACE);
        var gName = parser.ConsumeAndGetNextWordWithoutWhiteSpaces();

        var goodBject = new GoodBject(gName);

        if (parent == null)
        {
            GoodBjects.Add(goodBject);
        }
        else
        {
            parent.Children.Add(goodBject);
        }

        parser.MoveToNextLine();

        parser.ConsumeTabs(tabCount + 1);
        parser.Consume("Components");
        parser.MoveToNextLine();

        while (!parser.HasReachedEnd && parser.GetNextWordWithoutSpaces() == "Component")
        {
            parser.ConsumeTabs(tabCount + 2);
            parser.Consume("Component");
            parser.Consume(SPACE);

            var componentType = parser.ConsumeAndGetNextWordWithoutWhiteSpaces();
            var type = typeFinder(componentType);
            if (type == null)
            {
                type = Type.GetType(componentType);
                if (type == null)
                {
                    throw new Exception($"{parser.LocationString}:\nComponent {componentType} not found");
                }
            }

            var component = Activator.CreateInstance(type) as Component;

            parser.MoveToNextLine();

            //custom component creation and consumption, for now just read some shit

            component.Parse(parser, tabCount);

            component.SetGoodBject(goodBject);

            goodBject.Components.Add(component);
        }

        parser.ConsumeTabs(tabCount + 1);
        parser.Consume("Children");
        parser.MoveToNextLine();

        while (!parser.HasReachedEnd && parser.CanConsumeTabs(tabCount + 2) && parser.GetNextWordWithoutSpaces() == "GoodBject")
        {
            ParseGoodBjectRecursively(tabCount + 2, goodBject);
        }
    }
}