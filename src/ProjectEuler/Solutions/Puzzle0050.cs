using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0050 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(1_000_000);

        var hashes = primes.ToHashSet();
        
        var maxSum = 0L;

        var length = 0;

        for (var i = 0; i < primes.Count; i++)
        {
            var sum = 0L;

            for (var j = i; j < primes.Count; j++)
            {
                sum += primes[j];

                if (sum > 1_000_000)
                {
                    break;
                }

                if (hashes.Contains(sum) && sum > maxSum && j - i > length)
                {
                    length = j - i;

                    maxSum = sum;
                }
            }
        }

        return maxSum.ToString("N0");
    }
}