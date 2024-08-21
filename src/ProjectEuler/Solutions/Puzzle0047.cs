using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0047 : Puzzle
{
    private const int Horizon = 150_000;

    private const int SequenceLength = 4;
    
    public override string GetAnswer()
    {
        var count = 0;
        
        var primeCount = new byte[Horizon + 1];

        for (var i = 2; i <= Horizon; i++)
        {
            if (primeCount[i] == SequenceLength)
            {
                count++;
                if (count == SequenceLength)
                {
                    return (i - SequenceLength + 1).ToString("N0");
                }
            }
            else
            {
                count = 0;
                if (0 == primeCount[i])
                {
                    for (var j = i; j <= Horizon; j += i)
                    {
                        primeCount[j]++;
                    }
                }
            }
        }

        return "Unknown";
    }
}