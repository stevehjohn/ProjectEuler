using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0061 : Puzzle
{
    private readonly Dictionary<NumberType, List<long>> _numbers = [];
    
    public override string GetAnswer()
    {
        GenerateShapedNumbers();
        
        throw new NotImplementedException();
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