using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0001 : Puzzle
{
    public override string GetAnswer()
    {
        var sum = 0;

        for (var i = 1; i < 1_000; i++)
        {
            if (i % 3 == 0 || i % 5 == 0)
            {
                sum += i;
            }
        }

        return sum.ToString();
    }
}