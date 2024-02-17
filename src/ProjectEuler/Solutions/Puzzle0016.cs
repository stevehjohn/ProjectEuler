using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0016 : Puzzle
{
    public override string GetAnswer()
    {
        var number = BigInteger.Pow(2, 1_000);

        var digits = number.ToString();

        var result = 0L;
        
        foreach (var digit in digits)
        {
            result += digit - '0';
        }

        return result.ToString("N0");
    }
}