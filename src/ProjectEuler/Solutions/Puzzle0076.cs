using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0076 : Puzzle
{
    private const int Target = 100;
    
    public override string GetAnswer()
    {
        var partitions = new long[Target + 1];

        partitions[0] = 1;

        for (var k = 1; k < Target; k++)
        {
            for (var j = k ; j <= Target; j++)
            {
                partitions[j] += partitions[j - k];
            }
        }

        return partitions[Target].ToString("N0");
    }
}