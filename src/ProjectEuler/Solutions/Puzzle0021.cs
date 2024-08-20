using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0021 : Puzzle
{
    public override string GetAnswer()
    {
        var sum = 0;

        for (var i = 2; i < 10_000; i++)
        {
            var l = GetSumOfDivisors(i);

            var r = GetSumOfDivisors(l);

            if (r == i && l != r)
            {
                sum += i;
            }
        }

        return sum.ToString("N0");
    }

    private static int GetSumOfDivisors(int number)
    {
        var sum = 0;
        
        for (var i = 1; i < number; i++)
        {
            if (number % i == 0)
            {
                sum += i;
            }
        }

        return sum;
    }
}