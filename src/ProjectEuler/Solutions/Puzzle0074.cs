using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0074 : Puzzle
{
    private static readonly HashSet<long> Chain = [];

    private static readonly Dictionary<long, long> Cache = [];
    
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

    private static int GetChainLength(long number)
    {
        Chain.Clear();

        Chain.Add(number);

        long sum;
        
        do
        {
            sum = 0L;

            if (Cache.TryGetValue(number, out var value))
            {
                sum += value;

                number = value;
            }
            else
            {
                var text = number.ToString();
            
                for (var i = 0; i < text.Length; i++)
                {
                    var factorial = (long) Maths.Factorial(text[i] - '0');

                    sum += factorial;
                }
                    
                Cache.Add(number, sum);

                number = sum;
            }

        } while (Chain.Add(sum));

        return Chain.Count;
    }
}