using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0051 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(1_000_000);

        foreach (var prime in primes)
        {
            var strings = GenerateWildcardStrings(prime);

            if (strings.Count == 0)
            {
                continue;
            }

            var answer = CreatesEightPrimesAndRetunsFirst(strings, prime); 
            
            if (answer > 0)
            {
                return answer.ToString("N0");
            }
        }

        return "Unknown";
    }

    private static long CreatesEightPrimesAndRetunsFirst(List<string> strings, long prime)
    {
        var length = prime.ToString().Length;
        
        foreach (var number in strings)
        {
            var count = 0;

            var first = 0;
            
            for (var i = 0; i <= 9; i++)
            {
                var candidate = int.Parse(number.Replace('*', (char) ('0' + i)));

                if (candidate.ToString().Length < length)
                {
                    continue;
                }

                if (Maths.IsPrime(candidate))
                {
                    if (first == 0)
                    {
                        first = candidate;
                    }

                    count++;
                }
            }
            
            if (count == 8)
            {
                return first;
            }
        }

        return 0;
    }

    private static List<string> GenerateWildcardStrings(long prime)
    {
        var number = prime.ToString();

        var strings = new List<string>();

        if (number.Length < 3)
        {
            return strings;
        }

        GenerateWildcardStrings(strings, prime.ToString(), 0);

        return strings;
    }

    private static void GenerateWildcardStrings(List<string> strings, string number, int index)
    {
        if (index > 0)
        {
            var count = number.CountCharacters('*');
            
            if (count > 1 && count < number.Length)
            {
                strings.Add(number);
            }
        }

        for (var i = index; i < number.Length; i++)
        {
            var wildcard = $"{number[..i]}*{number[(i + 1)..]}";

            GenerateWildcardStrings(strings, wildcard, i + 1);
        }
    }
}