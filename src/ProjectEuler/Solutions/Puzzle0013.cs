using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0013 : Puzzle
{
    public override string GetAnswer()
    {
        LoadInput();

        var sum = new BigInteger();

        foreach (var line in Input)
        {
            sum += BigInteger.Parse(line);
        }
        
        return sum.ToString("N0")[..13];
    }
}