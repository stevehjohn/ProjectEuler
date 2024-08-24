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

        var a0 = (int) Math.Sqrt(number);

        var an = (int) Math.Sqrt(number);

        var mn = 0d;

        var dn = 1d;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (a0 != Math.Sqrt(number))
        {
            while (an != 2 * a0)
            {
                mn = dn * an - mn;
                
                dn = (number - mn * mn) / dn;

                an = (int) ((a0 + mn) / dn);

                period++;
            }
        }

        return period;
    }
}