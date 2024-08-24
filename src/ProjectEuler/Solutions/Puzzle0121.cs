using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0121 : Puzzle
{
    private const int MaxRounds = 63;
    
    public override string GetAnswer()
    {
        var wins = GenerateWins();

        var denominator = Maths.Factorial(MaxRounds + 1);

        var numerator = 0L;
        
        foreach (var win in wins)
        {
            numerator += GetNumeratorForRound(win);
        }

        // ReSharper disable once IntDivisionByZero
        return (denominator / numerator).ToString("N0");
    }

    private static long GetNumeratorForRound(ulong round)
    {
        var numerator = 1;
        
        for (var i = 0; i < MaxRounds; i++)
        {
            if ((round & (1ul << i)) == 0)
            {
                numerator *= i + 1;
            }
        }

        return numerator;
    }

    private static IEnumerable<ulong> GenerateWins()
    {
        var i = 1;

        var target = MaxRounds / 2 + 1;

        var grayCode = 0u;
        
        while ((grayCode & (1ul << MaxRounds)) == 0)
        {
            grayCode = (uint) (i ^ (i >> 1)); 
            
            if (BitOperations.PopCount(grayCode) >= target)
            {
                yield return grayCode;
            }

            i++;
        }
    }
}