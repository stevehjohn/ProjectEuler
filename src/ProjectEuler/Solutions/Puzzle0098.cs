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

        var anagrams = FindAnagrams(words).OrderBy(a => a.Left.Length);

        var squares = GetSquares((long) Math.Pow(10, anagrams.Max(a => a.Left.Length)));
        
        

        return "0";
    }

    private static List<(long Number, int Length)> GetSquares(long max)
    {
        var squares = new List<(long Number, int Length)>();

        var i = 1;
        
        while (true)
        {
            var square = i * i;

            if (square > max)
            {
                break;
            }
            
            squares.Add((square, (int) Math.Log10(square) + 1));

            i++;
        }

        return squares;
    }

    private static List<(string Left, string Right)> FindAnagrams(List<string> words)
    {
        var result = new List<(string Left, string Right)>();

        var leftFrequencies = new int[26];
        
        var rightFrequencies = new int[26];

        var found = new HashSet<string>();
        
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

                if (anagrams && ! found.Contains(left))
                {
                    result.Add((left, right));

                    found.Add(left);

                    found.Add(right);
                }
            }
        }
        
        return result;
    }
}