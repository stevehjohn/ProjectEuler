using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0053 : Puzzle
{
    public override string GetAnswer()
    {
        var count = 0;
        
        for (var n = 2; n <= 100; n++)
        {
            for (var r = 1; r < n; r++)
            {
                var combinations = Maths.Factorial(n) / (Maths.Factorial(r) * Maths.Factorial(n - r));

                if (combinations > 1_000_000)
                {
                    count++;
                }
            }
        }

        return count.ToString("N0");
    }
}