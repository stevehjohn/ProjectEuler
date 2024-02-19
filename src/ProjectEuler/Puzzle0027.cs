using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler;

[UsedImplicitly]
public class Puzzle0027 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(1_000_000).ToHashSet();

        var maxA = 0;

        var maxB = 0;

        var max = 0;
        
        for (var a = -999; a < 1_000; a++)
        {
            for (var b = -999; b < 1_000; b++)
            {
                var count = 0;
                
                for (var n = 0; n < 100; n++)
                {
                    if (primes.Contains(n * n + a * n + b))
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (count > max)
                {
                    maxA = a;

                    maxB = b;

                    max = count;
                }
            }
        }

        return (maxA * maxB).ToString("N0");
    }
}