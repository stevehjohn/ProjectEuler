using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0003 : Puzzle
{
    public override string GetAnswer()
    {
        var number = 600_851_475_143;

        var divisor = 2L;

        while (number > 1)
        {
            if (number % divisor == 0)
            {
                number /= divisor;
                
                continue;
            }

            if (divisor > Math.Sqrt(number))
            {
                divisor = number;
                
                continue;
            }

            divisor++;
        }

        return divisor.ToString("N0");
    }
}