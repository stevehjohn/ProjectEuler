using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

public class Puzzle0023 : Puzzle
{
    public override string GetAnswer()
    {
        var sum = 0;
        
        for (var i = 1; i < 28_123; i++)
        {
            Console.WriteLine(i);
            
            if (i % 2 == 1)
            {
                sum += i;
                
                continue;
            }

            for (var j = 1; j < 28_123; j++)
            {
                if (! IsAbundant(i) && ! IsAbundant(j) && i + j < 28_123)
                {
                    sum += i + j;
                }
            }
        }

        return sum.ToString("N0");
    }

    private static bool IsAbundant(int number)
    {
        return Maths.GetSumOfDivisors(number) > number;
    }
}