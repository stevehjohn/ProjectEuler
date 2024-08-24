using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0173 : Puzzle
{
    private const int MaxTiles = 1_000_000;
    
    public override string GetAnswer()
    {
        var count = 0;

        for (var holeSize = 1; holeSize < MaxTiles; holeSize++)
        {
            var complete = false;
            
            for (var layers = 1; layers < MaxTiles; layers++)
            {
                var tiles = GetTileCount(holeSize, layers);
                
                if (tiles == 0)
                {
                    if (layers == 1)
                    {
                        complete = true;
                    }
                    
                    break;
                }

                count++;
            }

            if (complete)
            {
                break;
            }
        }

        return count.ToString("N0");
    }

    private static int GetTileCount(int holeSize, int layers)
    {
        var totalTiles = 0;
        
        var sideSize = holeSize + 1;

        while (layers > 0)
        {
            var tiles = sideSize * 4;

            if (totalTiles + tiles <= MaxTiles)
            {
                totalTiles += tiles;

                layers--;

                sideSize += 2;
            }
            else
            {
                totalTiles = 0;
                
                break;
            }

        }
        
        return totalTiles;
    }
}