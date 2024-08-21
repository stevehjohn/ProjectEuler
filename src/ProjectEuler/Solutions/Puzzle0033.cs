using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0033 : Puzzle
{
    public override string GetAnswer()
    {
        var dp = 1L;

        var np = 1L;

        for (var c = 1; c < 10; c++)
        {
            for (var d = 1; d < c; d++)
            {
                for (var n = 1; n < d; n++)
                {
                    if ((n * 10 + c) * d == (c * 10 + d) * n)
                    {
                        np *= n;
                        
                        dp *= d;
                    }
                }
            }
        }

        var result = dp / Maths.GreatestCommonFactor(np, dp);

        return result.ToString("N0");
    }
}