using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0023 : Puzzle
{
    private readonly List<int> _abundant = new();
    
    public override string GetAnswer()
    {
        var sum = 0;
        
        for (var i = 1; i < 28_123; i++)
        {
            if (IsAbundant(i))
            {
                _abundant.Add(i);
            }
        }

        // for (var i = 1; i < 28_123; i++)
        // {
        //     if (! IsSumOfAbundant(i))
        //     {
        //         sum += i;
        //     }
        // }

        return sum.ToString("N0");
    }

    private bool IsSumOfAbundant(int number)
    {
        foreach (var l in _abundant)
        {
            if (l > number)
            {
                return false;
            }

            foreach (var r in _abundant)
            {
                if (l + r > number)
                {
                    break;
                }

                if (l + r == number)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsAbundant(int number)
    {
        return Maths.GetSumOfDivisors(number) > number;
    }
}