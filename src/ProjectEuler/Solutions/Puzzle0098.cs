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

        var anagrams = FindAnagrams(words).OrderBy(a => a.Left.Length).ToList();

        var allSquares = GetSquares((long) Math.Pow(10, anagrams.Max(a => a.Left.Length)));

        var lastLength = 0;

        List<string> relevantSquares = null;

        var max = 0L;
        
        foreach (var anagram in anagrams)
        {
            var length = anagram.Left.Length;
            
            if (length != lastLength)
            {
                relevantSquares = allSquares.Where(s => s.Length == length).Select(s => s.Number).ToList();

                lastLength = length;
            }

            max = Math.Max(FindMaxMapping(anagram, relevantSquares, length), max);
        }

        return max.ToString("N0");
    }

    private static long FindMaxMapping((string Left, string Right) anagram, List<string> relevantSquares, int length)
    {
        var mapping = new int[26];

        var right = new char[length];

        var max = 0;
        
        foreach (var square in relevantSquares)
        {
            for (var i = 0; i < length; i++)
            {
                mapping[anagram.Left[i] - 'A'] = square[i] - '0';
            }
            
            for (var i = 0; i < length; i++)
            {
                right[i] = (char) (mapping[anagram.Right[i] - 'A'] + '0');
            }

            var result = new string(right);

            if (result == square)
            {
                continue;
            }

            if (relevantSquares.Contains(result))
            {
                Console.WriteLine($"{square} {result}");
                max = int.Parse(result);
            }
        }

        return max;
    }

    private static List<(string Number, int Length)> GetSquares(long max)
    {
        var squares = new List<(string Number, int Length)>();

        var i = 1;
        
        while (true)
        {
            var square = i * i;

            if (square > max)
            {
                break;
            }

            var digits = square.ToString();
            
            squares.Add((digits, digits.Length));

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