using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0097 : Puzzle
{
    public override string GetAnswer()
    {
        var divisor = (long) Math.Pow(10, 10);

        var number = 28_433L;

        for (var i = 0; i < 7_830_457; i++)
        {
            number *= 2;

            number %= divisor;
        }

        number += 1;

        number %= divisor;

        return number.ToString("N0");
    }
}