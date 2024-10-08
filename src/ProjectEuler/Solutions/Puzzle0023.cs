using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0023 : Puzzle
{
    private const int MagicNumber = 28_123;
    
    private readonly List<int> _abundant = new();

    private readonly HashSet<int> _sums = new();
    
    public override string GetAnswer()
    {
        var sum = 0;
        
        for (var i = 1; i < MagicNumber; i++)
        {
            if (IsAbundant(i))
            {
                _abundant.Add(i);
            }
        }

        foreach (var l in _abundant)
        {
            foreach (var r in _abundant)
            {
                _sums.Add(l + r);

                if (l + r > MagicNumber)
                {
                    break;
                }
            }
        }
        
        for (var i = 1; i < MagicNumber; i++)
        {
            if (! _sums.Contains(i))
            {
                sum += i;
            }
        }

        return sum.ToString("N0");
    }

    private static bool IsAbundant(int number)
    {
        return Maths.GetSumOfDivisors(number) > number;
    }
}