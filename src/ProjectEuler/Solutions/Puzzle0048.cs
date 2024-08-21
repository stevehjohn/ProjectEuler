using System.Numerics;
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
            result += (long) ModPow(i, i, 10_000_000_000);

            result %= 10_000_000_000;
        }

        return result.ToString("N0");
    }

    private static BigInteger ModPow(BigInteger b, BigInteger e, BigInteger m)
    {
        var r = new BigInteger(1);

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