using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0014 : Puzzle
{
    public override string GetAnswer()
    {
        var maxLength = 0;

        var maxNumber = 0;
        
        for (var i = 1_000_000; i > 500_000; i--)
        {
            var length = GetSequenceLength(i);
            
            if (length > maxLength)
            {
                maxLength = length;

                maxNumber = i;
            }
        }

        return maxNumber.ToString("N0");
    }

    private static int GetSequenceLength(long number)
    {
        var length = 0;

        while (number != 1)
        {
            if (number % 2 == 0)
            {
                number /= 2;
            }
            else
            {
                number = number * 3 + 1;
            }

            length++;
        }

        return length;
    }
}