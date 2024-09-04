using GoodGine;
using GoodGine.ConsoleApp;
using GoodGine.TextParsing;
using static GoodGine.TextParsing.TextParser;

public class Player : Component
{
    private Renderer renderer;

    private Camera camera;

    public override void Parse(TextParser parser, int tabCount)
    {
        parser.ConsumeTabs(tabCount + 3);

        parser.Consume("ComponentReference"); parser.Consume(SPACE);
        parser.Consume("GoodGine.ConsoleApp.Camera"); parser.Consume(SPACE);

        var objName = parser.ConsumeAndGetNextWordWithoutWhiteSpaces();
        parser.MoveToNextLine();

        var go = Engine.FindObject(objName);

        camera = go.GetComponent<Camera>();
    }

    public override void Update()
    {
        if (!Console.KeyAvailable) return;

        ConsoleKeyInfo key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.Escape)
        {
            Engine.Quit();
        }

        if (key.Key == ConsoleKey.LeftArrow)
        {
            if (renderer.Column > 0) renderer.Column--;
            camera.Redraw();
        }

        if (key.Key == ConsoleKey.RightArrow)
        {
            if (renderer.Column + renderer.Width < Camera.COLUMNS - 1) renderer.Column++;
            camera.Redraw();
        }

        if (key.Key == ConsoleKey.UpArrow)
        {
            if (renderer.Line > 0) renderer.Line--;
            camera.Redraw();
        }

        if (key.Key == ConsoleKey.DownArrow)
        {
            if (renderer.Line + renderer.Height < Camera.LINES - 1) renderer.Line++;
            camera.Redraw();
        }
    }

    public override void Awake()
    {
        Console.WriteLine($"Awake {nameof(Player)}");

        renderer = GetComponent<Renderer>();
    }
}