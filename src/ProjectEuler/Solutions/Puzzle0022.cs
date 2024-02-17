using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0022 : Puzzle
{
    private List<string> _names = new();
    
    public override string GetAnswer()
    {
        LoadInput();

        ParseInput();

        var sum = 0L;

        for (var i = 0; i < _names.Count; i++)
        {
            sum += (i + 1) * GetNameScore(_names[i]);
        }

        return sum.ToString("N0");
    }

    private int GetNameScore(string name)
    {
        var score = 0;
        
        foreach (var c in name)
        {
            score += c - '@';
        }

        return score;
    }

    private void ParseInput()
    {
        var parts = Input[0].Split(',');

        _names = parts.Select(p => p.Trim('"')).OrderBy(n => n).ToList();
    }
}