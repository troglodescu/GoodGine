using Good.Engine.Components;
using SzUtils.Text;
using static SzUtils.Text.TextParser;

namespace Good;

public class Scene
{
    private TextParser parser;
    public List<GoodBject> GoodBjects { get; set; } = new List<GoodBject>();

    public void Load(string path)
    {
        parser = CreateParserForFile(path);

        GoodBjects.Clear();
        var tabCount = 0;
        while (!parser.HasReachedEnd)
        {
            ParseGoodBjectRecursively(tabCount, null);
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
            parser.MoveToNextLine();

            //custom component creation and consumption, for now just read some shit
            var component = new Transform2D();
            component.Parse(parser, tabCount);

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