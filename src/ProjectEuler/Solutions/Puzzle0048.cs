using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0048 : Puzzle
{
    public override string GetAnswer()
    {
        var result = 0L;

        for (var i = 1; i <= 1_000; i++)
        {
            result += ModPow(i, i, 10_000_000_000);

            result %= 10_000_000_000;
        }

        return result.ToString("N0");
    }

    private static long ModPow(long b, long e, long m)
    {
        var r = 1L;

        while (e > 0)
        {
            if ((e & 1) == 1)
            {
                r = r * b % m;
            }

            b = b * b % m;
            
            e >>= 1;
        }

        return r;
    }
}