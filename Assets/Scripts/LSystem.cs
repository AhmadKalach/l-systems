using System.Collections.Generic;
using System.Text;

public class LSystem
{
    private static float defaultAngle = 20f;

    string name;
    readonly List<Rule> rules;
    List<string> generations;
    int generationIndex;
    float angle;
    int systemId;
    List<char> forwardChars;

    public LSystem(int systemId, string name, string startString, List<Rule> rules, float angle, List<char> forwardChars)
    {
        this.systemId = systemId;
        this.name = name == null ? "system" : name;
        this.rules = rules;
        this.angle = angle;
        this.forwardChars = forwardChars;
        generations = new List<string>();
        generations.Add(startString);
    }

    public LSystem(int systemId, string name, string startString, List<Rule> rules, List<char> forwardChars) : this(systemId, name, startString, rules, defaultAngle, forwardChars)
    {
    }

    public int GetSystemId()
    {
        return systemId;
    }

    public string GetName()
    {
        return name;
    }

    public float GetAngle()
    {
        return angle;
    }

    public int GetGenerationIndex()
    {
        return generationIndex;
    }

    public List<char> GetForwardChars()
    {
        return forwardChars;
    }

    //to implement after doing skip generations
    public void SetToFirstGeneration()
    {
        generationIndex = 0;
    }

    public string GetCurrentGeneration()
    {
        return generations[generationIndex];
    }

    public string GetNextGeneration()
    {
        string toReturn = generations[generationIndex];
        generationIndex++;

        if (generations.Count < generationIndex)
        {
            toReturn = generations[generationIndex];
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < toReturn.Length; i++)
            {
                bool matched = false;
                foreach (Rule rule in rules)
                {
                    if (rule.Matches(toReturn[i]))
                    {
                        sb.Append(rule.GetOutput());
                        matched = true;
                    }
                }

                if (!matched)
                {
                    sb.Append(toReturn[i]);
                }
            }
            toReturn = sb.ToString();

            generations.Add(toReturn);
        }

        return toReturn;
    }

    public string GetPreviousGeneration()
    {
        if (generationIndex > 0)
        {
            generationIndex--;
            return generations[generationIndex];
        }

        return generations[0];
    }
}
