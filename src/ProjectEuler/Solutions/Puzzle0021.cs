using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0021 : Puzzle
{
    public override string GetAnswer()
    {
        var sum = 0;

        for (var i = 2; i < 10_000; i++)
        {
            var l = Maths.GetSumOfDivisors(i);

            var r = Maths.GetSumOfDivisors(l);

            if (r == i && l != r)
            {
                sum += i;
            }
        }

        return sum.ToString("N0");
    }

}