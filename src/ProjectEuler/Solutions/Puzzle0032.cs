using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0032 : Puzzle
{
    private readonly HashSet<long> _found = new();
    
    public override string GetAnswer()
    {
        var sum = 0;

        sum += ProcessMultiplicands(9, 99, 98, 988);

        sum += ProcessMultiplicands(9, 99, 98, 988);

        return sum.ToString("N0");
    }

    private int ProcessMultiplicands(int aLower, int aUpper, int bLower, int bUpper)
    {
        var sum = 0;
        
        for (var a = aLower; a < aUpper; a++)
        {
            for (var b = bLower; b < bUpper; b++)
            {
                var c = a * b;

                var cStr = c.ToString();

                if (cStr.Length > 5)
                {
                    break;
                }

                var all = new string($"{a}{b}{cStr}".ToCharArray().Order().ToArray());

                if (all == "123456789")
                {
                    if (_found.Add(c))
                    {
                        sum += c;
                    }
                }
            }
        }

        return sum;
    }
}