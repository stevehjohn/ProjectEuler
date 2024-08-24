using System.Numerics;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0074 : Puzzle
{
    private static readonly HashSet<BigInteger> Chain = [];
    
    public override string GetAnswer()
    {
        var count = 0;

        for (var i = 10; i < 1_000_000; i++)
        {
            if (GetChainLength(i) == 60)
            {
                count++;
            }
        }

        return count.ToString("N0");
    }

    private static int GetChainLength(long origin)
    {
        Chain.Clear();

        Chain.Add(origin);
        
        var text = origin.ToString();

        BigInteger sum;
        
        do
        {
            sum = new BigInteger(0);

            for (var i = 0; i < text.Length; i++)
            {
                sum += Maths.Factorial(text[i] - '0');
            }

            text = sum.ToString();

        } while (Chain.Add(sum));

        return Chain.Count;
    }
}