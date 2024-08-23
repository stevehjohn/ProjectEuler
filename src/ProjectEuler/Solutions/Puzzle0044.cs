using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0044 : Puzzle
{
    private List<int> _pentagonals;
    
    public override string GetAnswer()
    {
        GeneratePentagonals();

        var minimumDifference = int.MaxValue;
        
        for (var l = 0; l < _pentagonals.Count; l++)
        {
            for (var r = l + 1; r < _pentagonals.Count; r++)
            {
                if (IsPentagonal(_pentagonals[l] + _pentagonals[r]))
                {
                    var difference = _pentagonals[r] - _pentagonals[l];

                    if (IsPentagonal(difference) && difference < minimumDifference)
                    {
                        minimumDifference = difference;
                    }
                }
            }
        }

        return minimumDifference.ToString("N0");
    }

    private void GeneratePentagonals()
    {
        _pentagonals = [1];

        var i = 2;
        
        while (true)
        {
            var next = i * (3 * i - 1) / 2;
            
            _pentagonals.Add(next);

            if (next > 10_000_000)
            {
                break;
            }

            i++;
        }
    }
    
    private static bool IsPentagonal(long number)
    {
        var discriminant = 1d + 24 * number;

        var rootDiscriminant = Math.Sqrt(discriminant);

        var k = (1 + rootDiscriminant) / 6;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        return k == (int) k;
    }
}