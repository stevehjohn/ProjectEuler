using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0045 : Puzzle
{
    public override string GetAnswer()
    {
        var result = 286L;

        while (true)
        {
            var triangle = result * (result + 1) / 2;
            
            if (IsHexagonal(triangle) && IsPentagonal(triangle))
            {
                return triangle.ToString("N0");
            }

            result++;
        }
    }
    
    private static bool IsPentagonal(long number)
    {
        var discriminant = 1d + 24 * number;

        var rootDiscriminant = Math.Sqrt(discriminant);

        var k = (1 + rootDiscriminant) / 6;

        return k == (int) k;
    }

    private static bool IsHexagonal(long number)
    {
        var discriminant = 1d + 8 * number;

        var rootDiscriminant = Math.Sqrt(discriminant);

        var k = (1 + rootDiscriminant) / 4;

        return k == (int) k;
    }
}