using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0007 : Puzzle
{
    public override string GetAnswer()
    {
        var count = 1;

        var number = 3L;
        
        while (true)
        {
            if (Maths.IsPrime(number))
            {
                count++;

                if (count == 10_001)
                {
                    break;
                }
            }

            number += 2;
        }

        return number.ToString("N0");
    }
}