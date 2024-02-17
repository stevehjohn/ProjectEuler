using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0029 : Puzzle
{
    public override string GetAnswer()
    {
        var terms = new HashSet<BigInteger>();
        
        for (var a = 2; a < 101; a++)
        {
            for (var b = 2; b < 101; b++)
            {
                terms.Add(BigInteger.Pow(a, b));
            }
        }

        return terms.Count.ToString("N0");
    }
}