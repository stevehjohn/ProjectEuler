using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0075 : Puzzle
{
    private const int MaxLength = 1_500_000;
    
    public override string GetAnswer()
    {
        var triplets = GetPythagoreanTriplets();

        var result = CountUniqueLengths(triplets);
        
        return result.ToString("N0");
    }

    private static int CountUniqueLengths(HashSet<(int A, int B , int C)> triplets)
    {
        var lengthCounts = new Dictionary<int, int>();

        foreach (var triplet in triplets)
        {
            var length = triplet.A + triplet.B + triplet.C;

            if (! lengthCounts.TryAdd(length, 1))
            {
                lengthCounts[length]++;
            }
        }

        return lengthCounts.Count(x => x.Value == 1);
    }

    private static HashSet<(int A, int B, int C)> GetPythagoreanTriplets()
    {
        var result = new HashSet<(int A, int B, int C)>();

        var maxLength = Math.Sqrt(MaxLength);
        
        for (var m = 2; m < maxLength; m++)
        {
            for (var n = 1; n < m; n++)
            {
                var a = m * m - n * n;

                var b = 2 * m * n;

                var c = m * m + n * n;

                var length = a + b + c;
                
                var k = 1;

                while (k * length <= MaxLength)
                {
                    result.Add((Math.Min(a * k, b * k), Math.Max(a * k, b * k), c * k));

                    k++;
                }
            }
        }

        return result;
    }
}