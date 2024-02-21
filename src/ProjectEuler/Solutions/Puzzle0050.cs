using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0050 : Puzzle
{
    private List<long> _primes;

    public override string GetAnswer()
    {
        _primes = Maths.GetPrimes(1_000_000);

        var maxLength = 0;

        var value = 0L;

        foreach (var prime in _primes)
        {
            var length = GetSumLength(prime);

            if (length > maxLength)
            {
                maxLength = length;

                value = prime;
            }
        }

        return value.ToString("N0");
    }

    private int GetSumLength(long prime)
    {
    }
}