using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0057 : Puzzle
{
    public override string GetAnswer()
    {
        var denominator = new BigInteger(1);
        
        var numerator = new BigInteger(1);
        
        var denominatorDigits = new BigInteger(1);
        
        var numeratorDigits = new BigInteger(1);

        var count = 0;

        for (var i = 0; i < 1_000; i++)
        {
            numerator += 2 * denominator;

            denominator = numerator - denominator;

            if (numerator >= numeratorDigits)
            {
                numeratorDigits *= 10;
            }

            if (denominator >= denominatorDigits)
            {
                denominatorDigits *= 10;
            }

            if (numeratorDigits > denominatorDigits)
            {
                count++;
            }
        }

        return count.ToString("N0");
    }
}