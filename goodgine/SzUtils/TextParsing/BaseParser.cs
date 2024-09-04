namespace GoodGine.TextParsing;

public class TextParser
{
    public const char EOL = '\n', SPACE = ' ';
    public const string TAB = "    ";
    private List<string> lines;

    private int lineIndex = 0, columnIndex = 0;

    private string textName;

    private TextParser(string path)
    {
        textName = path;
        lines = File.ReadAllLines(path).ToList();
    }

    private TextParser(string text, List<string> lines)
    {
        textName = text;
        this.lines = lines;
    }

    public bool HasReachedEnd
    {
        get
        {
            if (lineIndex >= lines.Count)
            {
                return true;
            }
            return false;
        }
    }

    public string LocationString => $"Text:{textName}\nat line {lineIndex + 1} column {columnIndex}";
    private string currentLine => lines[lineIndex];

    private string remainingLine
    {
        get
        {
            if (columnIndex < currentLine.Length)
            {
                return currentLine.Substring(columnIndex);
            }
            return null;
        }
    }

    public static TextParser CreateParserForFile(string path)
    {
        return new TextParser(path);
    }

    public static TextParser CreateParserForString(string text)
    {
        var lines = text.Split(EOL).ToList();

        return new TextParser(text, lines);
    }

    public void Consume(char c)
    {
        Consume(c.ToString());
    }

    public void MoveToNextLine()
    {
        if (columnIndex != currentLine.Length)
        {
            ThrowException("Expected end of line but found more characters");
        }
        columnIndex = 0;
        lineIndex++;
    }

    public void Consume(string expected)
    {
        if (lineIndex >= lines.Count)
        {
            ThrowException($"Expected '{expected}' but reached end of file");
        }
        if (columnIndex + expected.Length > currentLine.Length)
        {
            ThrowException($"Expected '{expected}' but reached end of line");
        }

        var wordInLine = currentLine.Substring(columnIndex, expected.Length);

        if (wordInLine != expected)
        {
            ThrowException($"Expected '{expected}' but got '{wordInLine}'");
        }

        columnIndex += expected.Length;
    }

    public void ConsumeTabs(int tabCount)
    {
        for (int i = 0; i < tabCount; i++)
        {
            Consume(TAB);
        }
    }

    public string ConsumeAndGetNextWordWithoutWhiteSpaces()
    {
        if (currentLine[columnIndex] == ' ')
        {
            ThrowException("Expected word but got white space");
        }
        var word = string.Empty;
        while (columnIndex < currentLine.Length && currentLine[columnIndex] != ' ')
        {
            word += currentLine[columnIndex];
            columnIndex++;
        }

        return word;
    }

    public string GetNextWordWithoutSpaces()
    {
        var word = string.Empty;

        var c = columnIndex;
        while (c < currentLine.Length)
        {
            if (word.Length > 0 && currentLine[c] == SPACE) break;

            if (currentLine[c] != SPACE)
            {
                word += currentLine[c];
            }
            c++;
        }

        return word;
    }

    public bool CanConsumeTabs(int tabCount)
    {
        if (HasReachedEnd) return false;

        var totalWhiteSpaces = tabCount * TAB.Length;

        if (columnIndex + totalWhiteSpaces > currentLine.Length)
        {
            return false;
        }
        for (int i = columnIndex; i < totalWhiteSpaces; i++)
        {
            if (currentLine[i] != SPACE)
            {
                return false;
            }
        }
        return true;
    }

    public string ReadRestOfLine()
    {
        var restOfLine = currentLine.Substring(columnIndex);
        columnIndex = currentLine.Length;
        return restOfLine;
    }

    private void ThrowException(string message)
    {
        throw new Exception($"{LocationString}:\n{message}");
    }
}