using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0719 : Puzzle
{
    private const long Maximum = 1_000_000_000_000;
    
    public override string GetAnswer()
    {
        var number = 4L;

        var sum = 0L;
        
        while (true)
        {
            var square = (long) Math.Pow(number, 2);

            if (square > Maximum)
            {
                break;
            }

            if (IsSNumber(number, square))
            {
                sum += square;
            }

            number++;
        }

        return sum.ToString("N0");
    }

    private static bool IsSNumber(long number, long square)
    {
        if (square < number)
        {
            return false;
        }

        if (number == square)
        {
            return true;
        }

        var i = 10L;

        while (i < square)
        {
            var remainder = square % i;

            var quotient = square / i;
            
            if (remainder < number && IsSNumber(number - remainder, quotient))
            {
                return true;
            }

            i *= 10;
        }

        return false;
    }
}