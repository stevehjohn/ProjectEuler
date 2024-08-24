using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0064 : Puzzle
{
    public override string GetAnswer()
    {
        var counter = 0;
        
        for (var i = 0; i < 10_000; i++)
        {
            if (ContinuedFraction(i) % 2 != 0)
            {
                counter++;
            }
        }

        return counter.ToString("N0");
    }

    private static int ContinuedFraction(int number)
    {
        var period = 0;

        var squareRoot = (int) Math.Sqrt(number);

        var xN = (int) Math.Sqrt(number);

        var mantissaN = 0d;

        var denominator = 1d;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (squareRoot != Math.Sqrt(number))
        {
            while (xN != 2 * squareRoot)
            {
                mantissaN = denominator * xN - mantissaN;
                
                denominator = (number - mantissaN * mantissaN) / denominator;

                xN = (int) ((squareRoot + mantissaN) / denominator);

                period++;
            }
        }

        return period;
    }
}