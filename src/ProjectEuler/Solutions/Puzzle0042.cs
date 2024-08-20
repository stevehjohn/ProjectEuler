using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0042 : Puzzle
{
    private string[] _words;

    private readonly HashSet<int> _triangles = new();
    
    public override string GetAnswer()
    {
        LoadWords();

        GetTriangleNumbers(20);
        
        var count = 0;

        foreach (var word in _words)
        {
            var value = GetWordValue(word);

            if (_triangles.Contains(value))
            {
                count++;
            }
        }
        
        return count.ToString("N0");
    }

    private void GetTriangleNumbers(int max)
    {
        var n = 1;

        while (n < max)
        {
            _triangles.Add(n * (n + 1) / 2);

            n++;
        }
    }

    private static int GetWordValue(string word)
    {
        var value = 0;
        
        foreach (var letter in word)
        {
            value += letter - 'A' + 1;
        }

        return value;
    }

    private void LoadWords()
    {
        LoadInput();

        _words = Input[0].Replace("\"", string.Empty).Split(',');
    }
}