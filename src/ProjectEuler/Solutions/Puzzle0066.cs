using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0066 : Puzzle
{
    public override string GetAnswer()
    {
        var maxD = 0;
        
        var maxX = new BigInteger(0);

        for (var i = 2; i <= 1000; i++)
        {
            var sqrtD = (int) Math.Sqrt(i);

            if (sqrtD * sqrtD == i)
            {
                continue;
            }

            var m = new BigInteger(0);
            var d = new BigInteger(1);
            var a = new BigInteger(sqrtD);

            var first = new BigInteger(1);
            var second = new BigInteger(sqrtD);

            var denominator1 = new BigInteger(0);
            
            var denominator2 = new BigInteger(1);

            while (second * second - i * denominator2 * denominator2 != 1)
            {
                m = d * a - m;
                d = (i - m * m) / d;
                a = (sqrtD + m) / d;

                var next = a * second + first;
                var nextDenominator = a * denominator2 + denominator1;

                first = second;
                second = next;

                denominator1 = denominator2;
                denominator2 = nextDenominator;
            }

            if (second > maxX)
            {
                maxX = second;
                maxD = i;
            }
        }

        return maxD.ToString("N0");
    }
}