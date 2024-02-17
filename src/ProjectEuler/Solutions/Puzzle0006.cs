using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0006 : Puzzle
{
    public override string GetAnswer()
    {
        var sumOfSquares = 0;

        var squareOfSum = 0;
        
        for (var i = 1; i < 101; i++)
        {
            sumOfSquares += i * i;

            squareOfSum += i;
        }

        squareOfSum *= squareOfSum;

        return (squareOfSum - sumOfSquares).ToString("N0");
    }
}