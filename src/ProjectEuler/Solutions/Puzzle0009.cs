using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0009 : Puzzle
{
    public override string GetAnswer()
    {
        var a = 1L;

        var b = 1L;
        
        while (true)
        {
            var c = a * a + b * b;

            if (a + b + c == 1_000)
            {
                return (a * b * c).ToString("N0");
            }

            b++;

            if (b > 1000)
            {
                a++;

                b = a + 1;
            }
        }
    }
}