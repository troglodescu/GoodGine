using GoodGine.TextParsing;
using System.Text;
using static GoodGine.TextParsing.TextParser;

namespace GoodGine.ConsoleApp;

//this just resets the console and draws all game objects that have console transform and mesh renderer

public class Renderer : Component
{
    public int Line, Column;
    private List<string> image;
    public int Width => image[0].Length;
    public int Height => image.Count;

    public override void Parse(TextParser parser, int tabCount)
    {
        parser.ConsumeTabs(tabCount + 3);
        Line = int.Parse(parser.ConsumeAndGetNextWordWithoutWhiteSpaces());
        parser.Consume(SPACE);
        Column = int.Parse(parser.ConsumeAndGetNextWordWithoutWhiteSpaces());
        parser.MoveToNextLine();

        image = new();
        while (parser.CanConsumeTabs(tabCount + 3))
        {
            parser.ConsumeTabs(tabCount + 3);
            image.Add(parser.ReadRestOfLine());
            parser.MoveToNextLine();
        }
    }

    internal void Draw(List<string> matrix)
    {
        var lines = matrix.Count;
        var columns = matrix[0].Length;

        for (int i = Line, k = 0; i < Line + Height; i++, k++)
        {
            for (int j = Column, l = 0; j < Column + Width; j++, l++)
            {
                string line = matrix[i];
                var sb = new StringBuilder(line);
                sb[j] = image[k][l];

                matrix[i] = sb.ToString();
            }
        }
    }
}