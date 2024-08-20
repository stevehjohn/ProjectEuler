using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Extensions;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0055 : Puzzle
{
    private const int Max = 10_000;
    
    public override string GetAnswer()
    {
        var count = 0;

        for (var i = 1; i < Max; i++)
        {
            if (! WillPalindrome(i))
            {
                count++;
            }
        }

        return count.ToString("N0");
    }

    private static bool WillPalindrome(BigInteger number)
    {
        for (var i = 1; i < 50; i++)
        {
            var right = BigInteger.Parse(new string(number.ToString().Reverse().ToArray()));

            var result = number + right;
            
            if (result.ToString().IsPalindrome())
            {
                return true;
            }

            number = result;
        }

        return false;
    }
}