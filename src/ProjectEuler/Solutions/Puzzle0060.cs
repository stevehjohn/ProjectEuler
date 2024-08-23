using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0060 : Puzzle
{
    private List<(long Left, long Right)> _concatenationSet;
    
    public override string GetAnswer()
    {
        var primes = Maths.GetPrimes(10_000);

        GetPrimesThatConcatenateToAPrime(primes);

        var i = 1;
        
        foreach (var item in _concatenationSet)
        {
            Console.WriteLine($"Checking {i}/{_concatenationSet.Count}");

            i++;
            
            var chain = WalkChain([item.Left]);

            if (chain != null)
            {
                for (var l = 0; l < 5; l++)
                {
                    for (var r = l; r < 5; r++)
                    {
                        if (! ConcatenatesToPrime(chain[l], chain[r]))
                        {
                            goto next;
                        }
                    }
                }

                return "0";
            }
            
            next: ;
        }

        return "Unknown";
    }

    private List<long> WalkChain(List<long> chain, int depth = 0)
    {
        if (depth == 5)
        {
            return chain;
        }

        var items = _concatenationSet.Where(i => i.Left == chain.Last());

        foreach (var right in items)
        {
            WalkChain([..chain, right.Right], depth + 1);
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

    private bool ConcatenatesToPrime(long left, long right)
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