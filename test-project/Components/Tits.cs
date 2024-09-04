using Good;
using SzUtils.Text;

public class Tits : Component
{
    public override void Parse(TextParser parser, int tabCount)
    {
        parser.ConsumeTabs(tabCount + 3);
        parser.Consume("Fuck");
        parser.MoveToNextLine();
    }

    public override void Update()
    {
    }
}