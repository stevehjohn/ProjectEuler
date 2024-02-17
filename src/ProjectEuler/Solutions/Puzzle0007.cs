using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0007 : Puzzle
{
    public override string GetAnswer()
    {
        var count = 1;

        var number = 3L;
        
        while (true)
        {
            if (IsPrime(number))
            {
                count++;

                if (count == 10_001)
                {
                    break;
                }
            }

            number += 2;
        }

        return number.ToString("N0");
    }

    private static bool IsPrime(long number)
    {
        for (var i = 3; i < number; i += 2)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}