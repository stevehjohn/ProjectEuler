using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0049 : Puzzle
{
    private const int Increment = 3_330;
        
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(10_000);

        var hashes = primes.ToHashSet();

        var result = 0L;
        
        foreach (var prime in primes)
        {
            if (prime == 1_487)
            {
                continue;
            }

            if (hashes.Contains(prime + Increment) && hashes.Contains(prime + Increment * 2))
            {
                if (IsPermutation(prime))
                {
                    result = long.Parse($"{prime}{prime + Increment}{prime + Increment * 2}");
                }
            }
        }
        
        return result.ToString("N0");
    }

    private static bool IsPermutation(long number)
    {
        var left = new string(number.ToString().ToCharArray().OrderBy(c => c).ToArray());

        var right = new string((number + Increment).ToString().ToCharArray().OrderBy(c => c).ToArray());

        if (left != right)
        {
            return false;
        }

        right = new string((number + Increment * 2).ToString().ToCharArray().OrderBy(c => c).ToArray());

        return left == right;
    }
}