using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0056 : Puzzle
{
    public override string GetAnswer()
    {
        var max = 0;
        
        for (var a = 1; a < 100; a++)
        {
            for (var b = 1; b < 100; b++)
            {
                var number = BigInteger.Pow(a, b).ToString();

                var sum = 0;
                
                foreach (var c in number)
                {
                    sum += c - '0';
                }

                max = Math.Max(max, sum);
            }
        }

        return max.ToString("N0");
    }
}