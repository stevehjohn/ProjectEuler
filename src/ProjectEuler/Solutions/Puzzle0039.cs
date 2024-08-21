using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0039 : Puzzle
{
    public override string GetAnswer()
    {
        var max = 0;

        var maxPerimeter = int.MinValue;

        for (var perimeter = 2; perimeter <= 1_000; perimeter += 2)
        {
            var count = 0;

            for (var a = 2; a < perimeter / 3; a++)
            {
                if (perimeter * (perimeter - 2 * a) % (2 * (perimeter - a)) == 0)
                {
                    count++;
                }
            }

            if (count > max)
            {
                max = count;

                maxPerimeter = perimeter;
            }
        }

        return maxPerimeter.ToString("N0");
    }
}