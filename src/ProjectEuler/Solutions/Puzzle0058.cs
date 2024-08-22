using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0058 : Puzzle
{
    public override string GetAnswer()
    {
        var increment = 2;

        var number = 1;

        var total = 1;

        var primes = 0;
        
        while (true)
        {
            for (var i = 0; i < 4; i++)
            {
                number += increment;

                total++;

                if (Maths.IsPrime(number))
                {
                    primes++;
                }
            }

            increment += 2;

            if (increment > 6)
            {
                if (primes < total / 10)
                {
                    return (increment + 1).ToString("N0");
                }
            }
        }
    }
}