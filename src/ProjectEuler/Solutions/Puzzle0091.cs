using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0091 : Puzzle
{
    private const int GridSize = 50;
    
    public override string GetAnswer()
    {
        var count = 0;
        
        for (var x1 = 0; x1 <= GridSize; x1++)
        {
            for (var y1 = 0; y1 <= GridSize; y1++)
            {
                for (var x2 = 0; x2 <= GridSize; x2++)
                {
                    for (var y2 = 0; y2 <= GridSize; y2++)
                    {
                        if (y2 * x1 < y1 * x2 && IsRightAngle(x1, y1, x2, y2))
                        {
                            count++;
                        }
                    }
                }
            }
        }

        return count.ToString("N0");
    }

    private static bool IsRightAngle(int x1, int y1, int x2, int y2)
    {
        var a = Math.Pow(x1, 2) + Math.Pow(y1, 2);

        var b = Math.Pow(x2, 2) + Math.Pow(y2, 2);

        var c = Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2);

        // ReSharper disable CompareOfFloatsByEqualityOperator
        return a + b == c || b + c == a || c + a == b;
    }
}