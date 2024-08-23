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

            if (CreatesEightPrimes(strings))
            {
                return prime.ToString("N0");
            }
        }

        return "Unknown";
    }

    private static bool CreatesEightPrimes(List<string> strings)
    {
        foreach (var number in strings)
        {
            var count = 0;
            
            for (var i = 0; i <= 9; i++)
            {
                var candidate = int.Parse(number.Replace('*', (char) ('0' + i)));
                
                if (Maths.IsPrime(candidate))
                {
                    count++;
                }
            }
            
            if (count == 8)
            {
                return true;
            }
        }

        return false;
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