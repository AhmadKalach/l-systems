using System;

public class Rule
{
    char input;
    string output;

    public Rule(char input, string output)
    {
        this.input = input;
        this.output = output;
    }

    public bool Matches(char value)
    {
        return input.ToString().Equals(value.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public string GetOutput()
    {
        return output;
    }

    public char GetInput()
    {
        return input;
    }
}
