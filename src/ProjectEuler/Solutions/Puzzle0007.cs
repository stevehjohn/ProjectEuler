using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0007 : Puzzle
{
    public override string GetAnswer()
    {
        var count = 0;

        var number = 2L;
        
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

            number++;
        }

        return number.ToString("N0");
    }

    private static bool IsPrime(long number)
    {
        for (var i = 2; i < number; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}