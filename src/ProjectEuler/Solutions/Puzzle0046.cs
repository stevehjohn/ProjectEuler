using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0046 : Puzzle
{
    private List<long> _primes;
    
    public override string GetAnswer()
    {
        _primes = Maths.GetPrimes(10000);
        
        var i = 33;
        
        while (true)
        {
            if (i % 2 == 1 && IsComposite(i))
            {
                if (! IsPrimeAndTwiceASquare(i))
                {
                    break;
                }
            }

            i++;
        }

        return i.ToString("N0");
    }

    private static bool IsComposite(int number)
    {
        for (var d = 2; d < 100; d++)
        {
            if (number % d == 0)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsPrimeAndTwiceASquare(int number)
    {
        foreach (var prime in _primes)
        {
            for (var i = 1; i < 100; i++)
            {
                if (prime + 2 * i * i == number)
                {
                    return true;
                }
            }
        }

        return false;
    }
}