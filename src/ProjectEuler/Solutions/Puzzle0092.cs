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

    private bool ChainReturns89(int start)
    {
        var digits = start.ToString();
        
        while (true)
        {
            var sum = SumOfSquares(digits);
            
            if (sum == 1 || sum == 89)
            {
                return sum == 89;
            }

            digits = sum.ToString();
        }
}

    private static long SumOfSquares(string digits)
    {
        var sum = 0L;

        for (var i = 0; i < digits.Length; i++)
        {
            sum += (long) Math.Pow(digits[i] - '0', 2);
        }

        return sum;
    }
}