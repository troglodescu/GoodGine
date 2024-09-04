using GoodGine.TextParsing;

namespace GoodGine.ConsoleApp;

public class Camera : Component
{
    public const int LINES = 30, COLUMNS = 50;
    private List<string> matrix;

    public override void Parse(TextParser parser, int tabCount)
    {
        //parser.MoveToNextLine();
    }

    public override void Awake()
    {
        matrix = new();
    }

    public override void Start()
    {
        Redraw();
    }

    public void Redraw()
    {
        Clear();
        var objects = Engine.Scene.GoodBjects;
        foreach (var go in objects)
        {
            DrawGameObjectIfItHasConsoleRenderer(go);
        }

        foreach (var line in matrix)
        {
            Console.WriteLine(line);
        }
    }

    private void Clear()
    {
        Console.Clear();

        matrix.Clear();
        for (int i = 0; i < LINES; i++)
        {
            var str = string.Empty;
            for (int j = 0; j < COLUMNS; j++)
            {
                str += " ";
            }

            matrix.Add(str);
        }
    }

    private void DrawGameObjectIfItHasConsoleRenderer(GoodBject go)
    {
        foreach (var c in go.Components)
        {
            var renderer = c as Renderer;
            if (renderer == null) continue;

            renderer.Draw(matrix);
        }

        foreach (var c in go.Children)
        {
            DrawGameObjectIfItHasConsoleRenderer(c);
        }
    }
}