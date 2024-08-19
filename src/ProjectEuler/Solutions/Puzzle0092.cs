using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0092 : Puzzle
{
    private readonly HashSet<long> _history = new();
    
    public override string GetAnswer()
    {
        var count = 0;
        
        for (var i = 0; i < 10_000_000; i++)
        {
            if (ChainReturns89(i))
            {
                count++;
            }
        }
        
        return count.ToString();
    }

    private bool ChainReturns89(int start)
    {
        _history.Clear();

        var digits = start.ToString();

        while (true)
        {
            var sum = SumOfSquares(digits);

            if (! _history.Add(sum))
            {
                return sum == 89;
            }
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