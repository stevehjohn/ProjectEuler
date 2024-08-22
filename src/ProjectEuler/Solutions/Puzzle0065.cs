using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0065 : Puzzle
{
    public override string GetAnswer()
    {
        var fractions = new List<BigInteger> { 2 };

        var i = 1;

        while (fractions.Count < 100)
        {
            fractions.Add(1);
            
            fractions.Add(i * 2);
            
            fractions.Add(1);

            i++;
        }

        var numerator = new BigInteger(1);

        var denominator = fractions.Last();
        
        fractions.RemoveAt(fractions.Count - 1);

        for (i = fractions.Count - 1; i >= 0; i--)
        {
            (denominator, numerator) = (denominator * fractions[i] + numerator, denominator);
        }

        var denominatorString = denominator.ToString();

        var sum = 0;
        
        foreach (var digit in denominatorString)
        {
            sum += digit - '0';
        }

        return sum.ToString("N0");
    }
}