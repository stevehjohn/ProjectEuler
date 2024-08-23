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
                return chain.Sum(i => i.Number).ToString("N0");
            }
        }

        return "0";
    }
    
    private List<(long Number, NumberType Shape)> WalkChain(List<(long Number, NumberType Shape)> chain)
    {
        if (chain.Count == 6)
        {
            Console.WriteLine(string.Join(' ', chain.Select(n => n.Number)));
            
            if (chain.Last().Number % 100 == chain.First().Number / 100)
            {
                return chain;
            }
        }
        
        var end = chain.Last().Number % 100;

        foreach (var shape in Enum.GetValues<NumberType>())
        {
            if (chain.All(i => i.Shape != shape))
            {
                foreach (var number in _numbers[shape])
                {
                    if (number / 100 == end)
                    {
                        var result = WalkChain([..chain, (number, shape)]);

                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
            }
        }
        
        return null;
    }
    
    private void GenerateShapedNumbers()
    {
        _numbers.Clear();

        foreach (var shape in Enum.GetValues<NumberType>())
        {
            GenerateShapedNumbers(shape);
        }
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