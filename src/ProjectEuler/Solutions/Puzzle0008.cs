using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0008 : Puzzle
{
    
    public override string GetAnswer()
    {
        LoadInput();

        var digits = Input[0];
        
        var max = 0L;

        for (var i = 0; i < 988; i++)
        {
            var product = 1L;
            
            for (var d = 0; d < 13; d++)
            {
                product *= digits[i + d] - '0';
            }

            max = Math.Max(max, product);
        }

        return max.ToString("N0");
    }
}