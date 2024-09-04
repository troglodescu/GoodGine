using SzUtils.Text;

namespace Good;

public abstract class Component
{
    public abstract void Parse(TextParser parser, int tabCount);

    public abstract void Update();
}