using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0005 : Puzzle
{
    public override string GetAnswer()
    {
        var divisors = new List<long>();
        
        for (var i = 1; i < 21; i++)
        {
            divisors.Add(i);
        }

        return Maths.LowestCommonMultiple(divisors).ToString("N0");
    }
}