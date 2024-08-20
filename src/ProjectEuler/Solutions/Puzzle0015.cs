using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0015 : Puzzle
{
    private const int Steps = 40;

    private const int GridSize = 20;
    
    public override string GetAnswer()
    {
        var result = Maths.Factorial(Steps);

        result /= Maths.Factorial(GridSize) * Maths.Factorial(Steps - GridSize);

        return result.ToString("N0");
    }
}