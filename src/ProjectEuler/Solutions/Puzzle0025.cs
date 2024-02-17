using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0025 : Puzzle
{
    public override string GetAnswer()
    {
        var previous = new BigInteger(1);

        var current = new BigInteger(1);

        var index = 2;

        while (current.ToString().Length < 1_000)
        {
            var temp = current;

            current += previous;

            previous = temp;

            index++;
        }

        return index.ToString("N0");
    }
}