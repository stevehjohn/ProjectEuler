using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0077 : Puzzle
{
    private const int Max = 100;

    private const int TargetPartitions = 5_000;

    private List<long> _primes;
    
    public override string GetAnswer()
    {
        _primes = Maths.GetPrimes(Max);

        for (var i = 2; i <= Max; i++)
        {
            var partitions = CountPartitions(i);

            if (partitions > TargetPartitions)
            {
                return i.ToString("N0");
            }
        }

        return "Unknown";
    }

    private int CountPartitions(int target)
    {
        var partitions = new int[target + 1];

        partitions[0] = 1;
        
        foreach (var prime in _primes)
        {
            for (var j = prime ; j <= target; j++)
            {
                partitions[j] += partitions[j - prime];
            }
        }

        return partitions[target];
    }
}