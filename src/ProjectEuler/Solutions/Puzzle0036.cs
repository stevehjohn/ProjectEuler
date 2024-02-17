using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0036 : Puzzle
{
    public override string GetAnswer()
    {
        var sum = 0;
        
        for (var i = 1; i < 1_000_000; i++)
        {
            if (i.IsPalindrome())
            {
                var binary = Convert.ToString(i, 2);

                if (binary.IsPalindrome())
                {
                    sum++;
                }
            }
        }

        return sum.ToString("N0");
    }
}