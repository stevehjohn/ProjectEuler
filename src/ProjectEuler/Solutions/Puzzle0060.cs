using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0060 : Puzzle
{
    private const int ChainLength = 5;
    
    private List<(long Left, long Right)> _concatenationSet;
    
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(10_000);

        GetPrimesThatConcatenateToAPrime(primes);

        var i = 1;
        
        foreach (var item in _concatenationSet)
        {
            i++;
            
            var chain = WalkChain([item.Left, item.Right]);

            if (chain != null)
            {
                return chain.Sum().ToString("N0");
            }
        }

        return "Unknown";
    }

    private List<long> WalkChain(List<long> chain)
    {
        if (chain.Count == ChainLength)
        {
            return chain;
        }

        var last = chain.Last();
        
        var items = _concatenationSet.Where(i => i.Left == last);

        foreach (var right in items)
        {
            for (var i = 0; i < chain.Count - 1; i++)
            {
                if (! ConcatenatesToPrime(chain[i], right.Right))
                {
                    goto next;
                }
            }

            return WalkChain([..chain, right.Right]);
            
            next: ;
        }

        return null;
    }

    private void GetPrimesThatConcatenateToAPrime(List<long> primes)
    {
        _concatenationSet = new List<(long, long)>();
        
        for (var l = 0; l < primes.Count; l++)
        {
            var left = primes[l];
            
            for (var r = l + 1; r < primes.Count; r++)
            {
                var right = primes[r];

                if (ConcatenatesToPrime(left, right))
                {
                    _concatenationSet.Add((left, right));
                }
            }
        }
    }

    private static bool ConcatenatesToPrime(long left, long right)
    {
        var concatenated = long.Parse($"{left}{right}");

        if (Maths.IsPrime(concatenated))
        {
            concatenated = long.Parse($"{right}{left}");
                    
            if (Maths.IsPrime(concatenated))
            {
                return true;
            }
        }

        return false;
    }
}