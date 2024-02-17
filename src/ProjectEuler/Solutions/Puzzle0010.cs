using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0010 : Puzzle
{
    public override string GetAnswer()
    {
        var sum = 2L;

        for (var i = 3; i < 2_000_000; i += 2)
        {
            if (i % 100 == 0)
            {
                Console.WriteLine(i);
            }

            if (Maths.IsPrime(i))
            {
                sum++;
            }
        }

        return sum.ToString("N0");
    }
}