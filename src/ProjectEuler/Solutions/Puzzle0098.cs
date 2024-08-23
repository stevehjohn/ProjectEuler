using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0098 : Puzzle
{
    private List<string> _words;
    
    public override string GetAnswer()
    {
        LoadInput();

        _words = Input[0].Split(',').Select(l => l.Trim('"')).ToList();

        var anagrams = FindAnagrams();

        return "0";
    }

    private List<(string Left, string Right)> FindAnagrams()
    {
        var result = new List<(string Left, string Right)>();

        var leftFrequencies = new int[26];
        
        var rightFrequencies = new int[26];
        
        foreach (var left in _words)
        {
            if (left.Length < 1)
            {
                continue;
            }

            Array.Clear(leftFrequencies);

            foreach (var c in left)
            {
                leftFrequencies[c - 'A']++;
            }
            
            foreach (var right in _words.Where(l => l.Length == left.Length))
            {
                Array.Clear(rightFrequencies);
                
                if (left == right)
                {
                    continue;
                }

                foreach (var c in right)
                {
                    rightFrequencies[c - 'A']++;
                }

                var anagrams = true;
                
                for (var i = 0; i < 26; i++)
                {
                    if (leftFrequencies[i] != rightFrequencies[i])
                    {
                        anagrams = false;
                    }
                }

                if (anagrams)
                {
                    result.Add((left, right));
                }
            }
        }
        
        return result;
    }
}