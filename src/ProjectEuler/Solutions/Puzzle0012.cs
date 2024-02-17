using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0012 : Puzzle
{
    public override string GetAnswer()
    {
        var number = 1;

        var increment = 2;

        while (true)
        {
            number += increment;
            
            increment++;

            var divisors = CountDivisors(number);
            
            if (divisors > 500)
            {
                return divisors.ToString("N0");
            }
        }
    }

    private static int CountDivisors(int number)
    {
        var count = 0;
        
        for (var i = 1; i < number / 2; i++)
        {
            if (number % i == 0)
            {
                count++;
            }
        }

        return count;
    }
}