using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0009 : Puzzle
{
    public override string GetAnswer()
    {
        for (var a = 1; a < 1_000; a++)
        {
            for (var b = a + 1; b < 1_000; b++)
            {
                var c = 1_000 - a - b;

                if (a * a + b * b == c * c)
                {
                    return (a * b * c).ToString("N0");
                }
            }
        }

        return "?";
    }
}