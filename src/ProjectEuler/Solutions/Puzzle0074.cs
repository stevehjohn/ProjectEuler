using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0074 : Puzzle
{
    private static readonly HashSet<long> Chain = [];

    private static readonly Dictionary<long, long> Cache = [];

    private static readonly Dictionary<long, long> FactorialCache = [];
    
    public override string GetAnswer()
    {
        Cache.Clear();
        
        FactorialCache.Clear();
        
        InitialiseFactorialCache();
        
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

    private static void InitialiseFactorialCache()
    {
        FactorialCache.Add(0, 1);

        for (var i = 1; i < 10; i++)
        {
            FactorialCache.Add(i, (long) Maths.Factorial(i));
        }
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
                    var factorial = FactorialCache[text[i] - '0'];

                    sum += factorial;
                }
                    
                Cache.Add(number, sum);

                number = sum;
            }

        } while (Chain.Add(sum));

        return Chain.Count;
    }
}