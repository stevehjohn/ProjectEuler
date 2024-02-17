using JetBrains.Annotations;
using ProjectEuler.Infrastructure;
using ProjectEuler.Libraries;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0020 : Puzzle
{
    public override string GetAnswer()
    {
        var factorial = Maths.Factorial(100).ToString();

        var result = 0;
        
        foreach (var c in factorial)
        {
            result += c - '0';
        }
        
        return result.ToString("N0");
    }
}