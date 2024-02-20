using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0037 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(1_000_000);

        var hashes = primes.ToHashSet();

        var sum = 0L;
        
        foreach (var prime in primes)
        {
            var str = prime.ToString();

            if (str.Length < 2)
            {
                continue;
            }

            var passed = true;
            
            for (var i = 1; i < str.Length; i++)
            {
                if (! hashes.Contains(long.Parse(str[i..])))
                {
                    passed = false;

                    break;
                }
                
                if (! hashes.Contains(long.Parse(str[..^i])))
                {
                    passed = false;

                    break;
                }
            }

            if (passed)
            {
                sum += prime;
            }
        }

        return sum.ToString("N0");
    }
}