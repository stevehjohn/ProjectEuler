using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0060 : Puzzle
{
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(10_000);

        var concatenationSet = GetPrimesThatConcatenateToAPrime(primes);
        
        throw new NotImplementedException();
    }

    private List<(long First, long Second)> GetPrimesThatConcatenateToAPrime(List<long> primes)
    {
        var result = new List<(long, long)>();
        
        for (var l = 0; l < primes.Count; l++)
        {
            var left = primes[l];
            
            for (var r = l + 1; r < primes.Count; r++)
            {
                var right = primes[r];
                
                var concatenated = long.Parse($"{left}{right}");

                if (Maths.IsPrime(concatenated))
                {
                    result.Add((left, right));
                }
            }
        }

        return result;
    }
}