using GoodGine.Math;
using GoodGine.TextParsing;

namespace GoodGine;

using static TextParser;

public class Transform2D : Component
{
    public Vector2Double Position;

    public double Rotation;

    public override void Parse(TextParser parser, int tabCount)
    {
        parser.ConsumeTabs(tabCount + 3);
        Position = new Vector2Double();

        parser.Consume("Position");
        parser.Consume(SPACE);
        Position.x = double.Parse(parser.ConsumeAndGetNextWordWithoutWhiteSpaces());
        parser.Consume(SPACE);
        Position.y = double.Parse(parser.ConsumeAndGetNextWordWithoutWhiteSpaces());

        parser.MoveToNextLine();

        parser.ConsumeTabs(tabCount + 3);

        parser.Consume("Rotation");
        parser.Consume(SPACE);
        Rotation = double.Parse(parser.ConsumeAndGetNextWordWithoutWhiteSpaces());
        parser.MoveToNextLine();
    }

    public override void Awake()
    {
        Console.WriteLine($"Awake {nameof(Transform2D)} {Position.x} {Position.y}");
    }

    public override void Update()
    {
    }
}