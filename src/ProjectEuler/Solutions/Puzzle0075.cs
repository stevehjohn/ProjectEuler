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

        foreach (var triplet in triplets)
        {
            Console.WriteLine(triplet);
        }

        var result = CountUniqueLengths(triplets);
        
        return result.ToString("N0");
    }

    private static int CountUniqueLengths(List<(int A, int B , int C)> triplets)
    {
        var unique = new HashSet<int>();
        
        foreach (var triplet in triplets)
        {
            var length = triplet.A + triplet.B + triplet.C;

            unique.Add(length);
        }

        return unique.Count;
    }

    private static List<(int A, int B, int C)> GetPythagoreanTriplets()
    {
        var result = new List<(int, int, int)>();

        var maxLength = Math.Sqrt(MaxLength);
        
        for (var m = 2; m < maxLength; m++)
        {
            for (var n = 1; n < m; n++)
            {
                var a = m * m - n * n;

                var b = 2 * m * n;

                var c = m * m + n * n;

                var length = a + b + c;
                
                if (length > MaxLength)
                {
                    break;
                }

                result.Add((a, b, c));

                var k = 2;

                while (k * length < MaxLength)
                {
                    result.Add((a * k, b * k, c * k));

                    k++;
                }
            }
        }

        return result;
    }
}