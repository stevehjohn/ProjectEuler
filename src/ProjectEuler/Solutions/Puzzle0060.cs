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

        foreach (var item in _concatenationSet)
        {
            WalkChain(item.Right);
        }
        
        throw new NotImplementedException();
    }

    private void WalkChain(long left, int depth = 0)
    {
        if (depth == 4)
        {
        }

        var items = _concatenationSet.Where(i => i.Left == left);

        foreach (var item in items)
        {
            WalkChain(item.Right, depth + 1);
        }
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
                
                var concatenated = long.Parse($"{left}{right}");

                if (Maths.IsPrime(concatenated))
                {
                    _concatenationSet.Add((left, right));
                }
            }
        }
    }
}