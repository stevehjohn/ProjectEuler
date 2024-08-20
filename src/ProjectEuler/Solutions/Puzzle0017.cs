using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0017 : Puzzle
{
    private const int Max = 1_000;
    
    public override string GetAnswer()
    {
        var count = 0;
        
        for (var i = 1; i <= Max; i++)
        {
            count += i.NumberToEnglish().Replace(" ", string.Empty).Length;
        }

        return count.ToString("N0");
    }
}