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
                        if ((x1 != x2 || y1 != y2) && x1 * (y2 - y1) + y1 * (x1 - x2) >= 0 &&
                            x2 * (y1 - y2) + y2 * (x2 - x1) >= 0)
                        {
                            count++;
                        }
                    }
                }
            }
        }

        return count.ToString("N0");
    }
}