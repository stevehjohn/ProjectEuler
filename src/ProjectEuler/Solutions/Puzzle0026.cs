using System.Numerics;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

public class Puzzle0026 : Puzzle
{
    public override string GetAnswer()
    {
        var max = 0;

        for (var i = 2; i < 1_000; i++)
        {
            if (GetCycleLength(i) > max)
            {
                max = i;
            }
        }

        return max.ToString("N0");
    }

    private static int GetCycleLength(int number)
    {
        while (number % 2 == 0)
        {
            number /= 2;
        }

        while (number % 5 == 0)
        {
            number /= 5;
        }

        if (number == 1)
        {
            return 0;
        }

        var cycle = 1;

        BigInteger value;

        do
        {
            var str = new string('9', cycle);

            value = BigInteger.Parse(str);

            cycle++;

        } while (value % number != 0);

        return cycle;
    }
}