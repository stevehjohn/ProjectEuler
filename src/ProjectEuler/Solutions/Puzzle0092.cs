using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0092 : Puzzle
{
    public override string GetAnswer()
    {
        var count = 0L;
        
        for (var i = 1; i < 10_000_000; i++)
        {
            if (ChainReturns89(i))
            {
                count++;
            }
        }
        
        return count.ToString("N0");
    }

    private bool ChainReturns89(long value)
    {
        while (true)
        {
            value = SumOfSquares(value);
            
            if (value == 1 || value == 89)
            {
                return value == 89;
            }
        }
}

    private static long SumOfSquares(long digits)
    {
        var sum = 0L;

        while (digits != 0)
        {
            sum += digits % 10 * (digits % 10);

            digits /= 10;
        }

        return sum;
    }
}