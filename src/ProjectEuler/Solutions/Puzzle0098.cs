using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0098 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        var words = Input[0].Split(',').Select(l => l.Trim('"')).ToList();

        var anagrams = FindAnagrams(words);

        return "0";
    }

    private static List<(string Left, string Right)> FindAnagrams(List<string> words)
    {
        var result = new List<(string Left, string Right)>();

        var leftFrequencies = new int[26];
        
        var rightFrequencies = new int[26];
        
        foreach (var left in words)
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
            
            foreach (var right in words.Where(l => l.Length == left.Length))
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