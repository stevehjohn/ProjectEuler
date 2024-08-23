using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0061 : Puzzle
{
    private readonly Dictionary<NumberType, HashSet<long>> _numbers = [];
    
    public override string GetAnswer()
    {
        GenerateShapedNumbers();
        
        foreach (var oct in _numbers[NumberType.Octagonal])
        {
            var chain = WalkChain([(oct, NumberType.Octagonal)]);

            if (chain != null)
            {
            }
        }

        return "0";
    }
    
    private List<(long Number, NumberType Shape)> WalkChain(List<(long Number, NumberType Shape)> chain)
    {
        if (chain.Count >= 5)
        {
            Console.WriteLine(string.Join(" ", chain.Select(c => $"{c.Number:0000} {c.Shape.ToString()[..3]}")));
        }

        if (chain.Count == 6)
        {
            return chain;
        }

        if (chain.All(i => i.Shape != NumberType.Heptagonal))
        {
            WalkChain(chain, NumberType.Heptagonal);
        }

        if (chain.All(i => i.Shape != NumberType.Hexagonal))
        {
            WalkChain(chain, NumberType.Hexagonal);
        }

        if (chain.All(i => i.Shape != NumberType.Pentagonal))
        {
            WalkChain(chain, NumberType.Pentagonal);
        }

        if (chain.All(i => i.Shape != NumberType.Square))
        {
            WalkChain(chain, NumberType.Square);
        }

        if (chain.All(i => i.Shape != NumberType.Triangle))
        {
            WalkChain(chain, NumberType.Triangle);
        }
        
        return null;
    }

    private void WalkChain(List<(long Number, NumberType Shape)> chain, NumberType shape)
    {
        var end = chain.Last().Number % 100;
        
        foreach (var number in _numbers[shape])
        {
            if (number / 100 == end)
            {
                WalkChain([..chain, (number, shape)]);
            }
        }
    }

    private void GenerateShapedNumbers()
    {
        _numbers.Clear();
        
        GenerateShapedNumbers(NumberType.Triangle);
        GenerateShapedNumbers(NumberType.Square);
        GenerateShapedNumbers(NumberType.Pentagonal);
        GenerateShapedNumbers(NumberType.Hexagonal);
        GenerateShapedNumbers(NumberType.Heptagonal);
        GenerateShapedNumbers(NumberType.Octagonal);
    }

    private void GenerateShapedNumbers(NumberType shape)
    {
        var origin = 1;
        
        _numbers.Add(shape, []);

        var list = _numbers[shape];

        while (true)
        {
            var number = Maths.GenerateShapedNumber(origin, shape);

            origin++;
            
            if (number < 1_000)
            {
                continue;
            }

            if (number > 9_999)
            {
                break;
            }
            
            list.Add(number);
        }
    }
}