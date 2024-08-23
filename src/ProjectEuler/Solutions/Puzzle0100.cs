using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0100 : Puzzle
{
    public override string GetAnswer()
    {
        var (blue, n) = (3L, 4L);

        while (n <= 1_000_000_000_000)
        {
            (blue, n) = (3 * blue + 2 * n - 2, 4 * blue + 3 * n - 3);
        }

        return blue.ToString("N0");
    }
}