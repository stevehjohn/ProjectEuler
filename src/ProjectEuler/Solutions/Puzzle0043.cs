using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0043 : Puzzle
{
    private static readonly int[] Primes = { 2, 3, 5, 7, 11, 13, 17 };
    public override string GetAnswer()
    {
        var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        var permutations = digits.GetPermutations();

        var sum = 0L;
        
        foreach (var permutation in permutations)
        {
            if ((permutation[3] - '0') % 2 != 0 || ! (permutation[5] is '0' or '5'))
            {
                continue;
            }

            var number = new string(permutation);
            
            if (DigitsDivisibleByPrimes(number))
            {
                sum += long.Parse(number);
            }
        }

        return sum.ToString("N0");
    }

    private static bool DigitsDivisibleByPrimes(string digits)
    {
        for (var i = 0; i < 7; i++)
        {
            var number = int.Parse($"{digits[i + 1]}{digits[i + 2]}{digits[i + 3]}");

            if (number % Primes[i] != 0)
            {
                return false;
            }
        }

        return true;
    }
}