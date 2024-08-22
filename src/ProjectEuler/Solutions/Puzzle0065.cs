using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0065 : Puzzle
{
    public override string GetAnswer()
    {
        var fractions = new List<long>();

        var i = 1;

        while (fractions.Count < 100)
        {
            fractions.Add(1);
            
            fractions.Add(i * 2);
            
            fractions.Add(1);

            i++;
        }

        var numerator = 1L;

        var denominator = fractions.Last();
        
        fractions.RemoveAt(fractions.Count - 1);

        foreach (var fraction in fractions)
        {
            (denominator, numerator) = (denominator * fraction + numerator, denominator);
        }

        var denominatorString = denominator.ToString();

        var sum = 0;
        
        foreach (var digit in denominatorString)
        {
            sum += digit - '0';
        }

        return sum.ToString("N0");
    }
}