using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0028 : Puzzle
{
    public override string GetAnswer()
    {
        var sum = 1;

        var increment = 2;

        var number = 1;
        
        while (increment < 1_002)
        {
            for (var i = 0; i < 4; i++)
            {
                number += increment;
                
                sum += number;
            }

            increment += 2;
        }

        return sum.ToString("N0");
    }
}