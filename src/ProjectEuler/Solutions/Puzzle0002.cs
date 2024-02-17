using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0002 : Puzzle
{
    public override string GetAnswer()
    {
        var previous = 1;

        var current = 1;

        var sum = 0L;
        
        while (true)
        {
            var fibonacci = previous + current;

            if (fibonacci > 4_000_000)
            {
                break;
            }

            if (fibonacci % 2 == 0)
            {
                sum += fibonacci;
            }

            previous = current;

            current = fibonacci;
        }

        return sum.ToString("N0");
    }
}